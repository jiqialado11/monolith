using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubContractors.Domain.Budget;
using System.Collections.Generic;

namespace SubContractors.Infrastructure.Persistence.TypeConfiguration
{
    public class PaymentTermConfiguration : IEntityTypeConfiguration<PaymentTerm>
    {
        public void Configure(EntityTypeBuilder<PaymentTerm> builder)
        {
            builder.ToTable("PaymentTerm", "budget");

            var list = new List<PaymentTerm>
            {
                new(1, "NoRestriction", true),
                new(2, "AfterClientPayOnly", true),
                new(3, "AfterClientPayOnlyOrExpirationDate", true)
            };
            builder.HasData(list);
        }
    }
}