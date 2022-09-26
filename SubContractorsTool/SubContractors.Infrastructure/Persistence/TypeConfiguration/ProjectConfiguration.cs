using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubContractors.Domain.Project;
using SubContractors.Domain.SubContractor.Staff;
using System.Collections.Generic;

namespace SubContractors.Infrastructure.Persistence.TypeConfiguration
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Project", "project");


            builder.HasOne(sub => sub.DeliveryManager)
                   .WithMany()
                   .HasForeignKey(sub => sub.DeliveryManagerId)
                   .IsRequired(false);
            builder.HasOne(sub => sub.ProjectManager)
                   .WithMany()
                   .HasForeignKey(sub => sub.ProjectManagerId)
                   .IsRequired(false);
            builder.HasOne(sub => sub.InvoiceApprover)
                .WithMany()
                .HasForeignKey(sub => sub.InvoiceApproverId)
                .IsRequired(false);

            builder.HasMany(sub => sub.Staffs)
                   .WithMany(stuff => stuff.Projects)
                   .UsingEntity<Dictionary<string, object>>("project.ProjectStaff", j => j.HasOne<Staff>()
                                                                                          .WithMany()
                                                                                          .OnDelete(DeleteBehavior.NoAction), j => j.HasOne<Project>()
                                                                                                                                    .WithMany()
                                                                                                                                    .OnDelete(DeleteBehavior.NoAction));
        }
    }
}