using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubContractors.Domain.Budget;

namespace SubContractors.Infrastructure.Persistence.TypeConfiguration
{
    public class BudgetGroupConfiguration : IEntityTypeConfiguration<BudgetGroup>
    {
        public void Configure(EntityTypeBuilder<BudgetGroup> builder)
        {
            builder.ToTable("BudgetGroup", "budget");
        }
    }
}