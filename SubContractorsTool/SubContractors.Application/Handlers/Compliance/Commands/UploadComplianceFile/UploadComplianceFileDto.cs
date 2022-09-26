using System;

namespace SubContractors.Application.Handlers.Compliance.Commands.UploadComplianceFile
{
    public class UploadComplianceFileDto
    {
        public Guid? Id { get; set; }
        public string Filename { get; set; }
    }
}
