using System.Collections.Generic;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using SubContractors.Application.Common;
using SubContractors.Common;

namespace SubContractors.Application.Handlers.Invoices.Commands.UploadSupportingDocuments
{
    public class UploadSupportingDocuments : IRequest<Result<IList<UploadSupportingDocumentsDto>>>
    {
        public IList<IFormFile> Files { get; set; }
    }

    public class UploadSupportingDocumentationValidator : AbstractValidator<UploadSupportingDocuments>
    {
        public UploadSupportingDocumentationValidator()
        {
            RuleFor(x => x.Files)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Invoice_File_Content);
        }
    }
}
