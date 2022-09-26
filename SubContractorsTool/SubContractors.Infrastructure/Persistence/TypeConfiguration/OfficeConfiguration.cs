using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubContractors.Domain.SubContractor;
using System.Collections.Generic;

namespace SubContractors.Infrastructure.Persistence.TypeConfiguration
{
    public class OfficeConfiguration : IEntityTypeConfiguration<Office>
    {
        public void Configure(EntityTypeBuilder<Office> builder)
        {
            builder.ToTable("Office", "subcontractors");

            var list = new List<Office>
            {
                new(1, "sample sales office", OfficeType.SalesOffice),
                new(2, "sample development office", OfficeType.DevelopmentOffice)
            };
            builder.HasData(list);
        }
    }
}