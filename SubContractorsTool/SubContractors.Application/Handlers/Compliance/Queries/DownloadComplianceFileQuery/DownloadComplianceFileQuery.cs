using System;
using FluentValidation;
using MediatR;
using SubContractors.Application.Common;
using SubContractors.Common;

namespace SubContractors.Application.Handlers.Compliance.Queries.DownloadComplianceFileQuery
{
    public class DownloadComplianceFileQuery:IRequest<Result<DownloadComplianceFileDto>>
    {
        public Guid? Id { get; set; }
    }

    public class DownloadComplianceFileQueryValidator : AbstractValidator<DownloadComplianceFileQuery>
    {
        public DownloadComplianceFileQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required);
        }
    }
}
