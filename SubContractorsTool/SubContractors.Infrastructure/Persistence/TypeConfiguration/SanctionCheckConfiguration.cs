using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubContractors.Domain.Check;

namespace SubContractors.Infrastructure.Persistence.TypeConfiguration
{
    public  class SanctionCheckConfiguration : IEntityTypeConfiguration<SanctionCheck>
    {
        public void Configure(EntityTypeBuilder<SanctionCheck> builder)
        {
            builder.ToTable("SanctionCheck", "checks");
            builder.HasOne(model => model.Approver)
                   .WithMany()
                   .HasForeignKey(model => model.ApproverId)
                   .IsRequired(false);


            builder.HasOne(model => model.Staff)
                   .WithMany(x=> x.SanctionChecks);

            builder.HasOne(model => model.SubContractor)
                   .WithMany(x => x.SanctionChecks);


        }
    }
}
