using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubContractors.Domain.Check;

namespace SubContractors.Infrastructure.Persistence.TypeConfiguration
{
    public class BackgroundCheckConfiguration :  IEntityTypeConfiguration<BackgroundCheck>
    {
        public void Configure(EntityTypeBuilder<BackgroundCheck> builder)
        {
            builder.ToTable("BackgroundCheck", "checks");
            builder.HasOne(model => model.Approver)
                .WithMany()
                .HasForeignKey(model => model.ApproverId)
                .IsRequired(false);

            builder.HasOne(model => model.Staff)
                .WithMany(x => x.BackgroundChecks);

           
        }
    }
}
