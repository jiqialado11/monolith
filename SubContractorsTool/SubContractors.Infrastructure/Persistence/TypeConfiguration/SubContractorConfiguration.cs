using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubContractors.Domain.SubContractor;
using SubContractors.Domain.SubContractor.Staff;
using System.Collections.Generic;

namespace SubContractors.Infrastructure.Persistence.TypeConfiguration
{
    public class SubContractorConfiguration : IEntityTypeConfiguration<SubContractor>
    {
        public void Configure(EntityTypeBuilder<SubContractor> builder)
        {
            builder.ToTable("SubContractor", "subcontractors");

            builder.HasOne(sub => sub.AccountManager)
                   .WithMany()
                   .HasForeignKey(sub => sub.AccountManagerId)
                   .IsRequired(false);

            builder.HasMany(sub => sub.Staffs)
                   .WithMany(stuff => stuff.SubContractors)
                   .UsingEntity<Dictionary<string, object>>("subcontractors.StaffSubContractor", j => j.HasOne<Staff>()
                                                                                                       .WithMany()
                                                                                                       .OnDelete(DeleteBehavior.NoAction), j => j.HasOne<SubContractor>()
                                                                                                                                                 .WithMany()
                                                                                                                                                 .OnDelete(DeleteBehavior.NoAction));

            builder.HasMany(sub => sub.Offices)
                   .WithMany(office => office.SubContractors)
                   .UsingEntity<Dictionary<string, object>>("subcontractors.OfficeSubContractor", j => j.HasOne<Office>()
                                                                                                        .WithMany()
                                                                                                        .OnDelete(DeleteBehavior.NoAction), j => j.HasOne<SubContractor>()
                                                                                                                                                  .WithMany()
                                                                                                                                                  .OnDelete(DeleteBehavior.NoAction));
        }
    }
}