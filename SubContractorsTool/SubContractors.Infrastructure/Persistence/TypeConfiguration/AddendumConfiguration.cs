using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubContractors.Domain.Agreement;

namespace SubContractors.Infrastructure.Persistence.TypeConfiguration
{
    public class AddendumConfiguration : IEntityTypeConfiguration<Addendum>
    {
        public void Configure(EntityTypeBuilder<Addendum> builder)
        {
            builder
                .ToTable("Addendum", "agreements");

            builder.HasOne(x => x.Agreement)
                .WithMany(x => x.Addenda)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(x => x.Rates)
                .WithOne(x => x.Addendum)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
