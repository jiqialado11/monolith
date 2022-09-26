using System;
using SubContractors.Common.EfCore;

namespace SubContractors.Domain.Compliance
{
    public class ComplianceFile : Entity<Guid>
    {
        public string Filename { get; set; }
        public long FileSize { get; set; }
        public byte[] FileContent { get; set; }
        public string FileType { get; set; }
        public int? ComplianceId { get; set; }
        public Compliance Compliance { get; set; }

        public void Create(Guid id, string filename, long fileSize, byte[] fileContent, string fileType)
        {
            Id = id;
            Filename = filename;
            FileSize = fileSize;
            FileContent = fileContent;
            FileType = fileType;
        }

        public void Delete()
        {
            IsDeleted = true;
        }
    }
}