using System;

namespace SubContractors.Application.Handlers.Invoices.Commands.UploadSupportingDocuments
{
    public class UploadSupportingDocumentsDto
    {
        public Guid? Id { get; set; }
        public string Filename { get; set; }
    }
}
