using SubContractors.Common.EfCore;
using System;

namespace SubContractors.Domain.Invoice
{
    public class SupportingDocument : Entity<Guid>
    {
        public SupportingDocument()
        { 
        }

        public SupportingDocument(Guid id)
        {
            Id = id;
        }

        public string Filename { get; set; }
        public long FileSize { get; set; }
        public byte[] FileContent { get; set; }
        public int? InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

        public void Create(Guid id, string filename, long fileSize, byte[] fileContent)
        {
            Id = id;
            Filename = filename;
            FileSize = fileSize;
            FileContent = fileContent;
        }

        public void Delete()
        {
            IsDeleted = true;
        }
    }
}