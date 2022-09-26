using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using SubContractors.Domain.Agreement;

namespace SubContractors.Infrastructure.Persistence.TypeConfiguration
{
    public class RateUnitConfiguration : IEntityTypeConfiguration<RateUnit>
    {
        public void Configure(EntityTypeBuilder<RateUnit> builder)
        {
            builder.ToTable("RateUnit", "agreements");

            var list = new List<RateUnit>
            {
                new(1, "per/hour"),
                new(2, "per/day"),
                new(3, "per/week"),
                new(4, "per/month"),
                new(5, "per/year")
            };
            builder.HasData(list);
        }
    }
}