using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using SubContractors.Application.Common;
using SubContractors.Common;

namespace SubContractors.Application.Handlers.Compliance.Commands.UploadComplianceFile
{
    public class UploadComplianceFile : IRequest<Result<UploadComplianceFileDto>>
    {
        public IFormFile File { get; set; }
    }

    public class UploadComplianceFilesValidator : AbstractValidator<UploadComplianceFile>
    {
        public UploadComplianceFilesValidator()
        {
            RuleFor(x => x.File)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required);

            RuleFor(x => x.File.Length)
                .NotNull()
                .GreaterThanOrEqualTo(1)
                .WithMessage(Constants.ValidationErrors.File_Length);

        }
    }

}
