using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubContractors.Domain.Invoice;

namespace SubContractors.Infrastructure.Persistence.TypeConfiguration
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("Invoice", "invoice");

            builder.HasOne(sub => sub.InvoiceFile)
                   .WithMany()
                   .HasForeignKey(sub => sub.InvoiceFileId)
                   .IsRequired(false);

            builder.HasMany(sub => sub.SupportingDocuments)
                   .WithOne(docs => docs.Invoice)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Addendum)
                   .WithMany(x => x.Invoices)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
