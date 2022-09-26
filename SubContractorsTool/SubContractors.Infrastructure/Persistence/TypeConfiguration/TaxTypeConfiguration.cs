using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubContractors.Domain.SubContractor.Tax;

namespace SubContractors.Infrastructure.Persistence.TypeConfiguration
{
    public class TaxTypeConfiguration : IEntityTypeConfiguration<TaxType>
    {
        public void Configure(EntityTypeBuilder<TaxType> builder)
        {
            builder.ToTable("TaxType", "tax");

            var list = new List<TaxType>
            {
                new(1, "W9", string.Empty),
                new(2, "W-8BEN", string.Empty)
            };
            builder.HasData(list);
        }
    }
}
