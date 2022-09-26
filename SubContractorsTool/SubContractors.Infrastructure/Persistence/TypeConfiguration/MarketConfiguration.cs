using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubContractors.Domain.SubContractor;
using System.Collections.Generic;

namespace SubContractors.Infrastructure.Persistence.TypeConfiguration
{
    public class MarketConfiguration : IEntityTypeConfiguration<Market>
    {
        public void Configure(EntityTypeBuilder<Market> builder)
        {
            builder.ToTable("Market", "subcontractors");

            var list = new List<Market>
            {
                new(1, "Russia"),
                new(2, "USA"),
                new(3, "Ukraine")
            };
            builder.HasData(list);
        }
    }
}