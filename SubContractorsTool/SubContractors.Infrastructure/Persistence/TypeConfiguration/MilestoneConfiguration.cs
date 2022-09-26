using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubContractors.Domain.Invoice;

namespace SubContractors.Infrastructure.Persistence.TypeConfiguration
{
    public class MilestoneConfiguration : IEntityTypeConfiguration<Milestone>
    {
        public void Configure(EntityTypeBuilder<Milestone> builder)
        {          
            builder.ToTable("Milestone", "invoice");

            builder.HasOne(p => p.Invoice)
                   .WithOne(a => a.MileStone)
                   .HasForeignKey<Invoice>(a => a.MilestoneId);
        }
    }
}
