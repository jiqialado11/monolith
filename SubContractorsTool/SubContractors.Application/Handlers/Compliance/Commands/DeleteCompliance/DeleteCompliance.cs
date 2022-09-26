using FluentValidation;
using MediatR;
using SubContractors.Application.Common;
using SubContractors.Common;
using SubContractors.Common.Redis;
using System.Collections.Generic;

namespace SubContractors.Application.Handlers.Compliance.Commands.DeleteCompliance
{
    public class DeleteCompliance : IRequest<Result<Unit>>, IInvalidateCacheRequest
    {
        public int? Id { get; set; }

        public List<string> GetDomainsIdentifiers()
        {
            return new List<string> { 
                                      typeof(Domain.Compliance.Compliance).FullName
                                     };
        }
    }

    public class DeleteComplianceValidator : AbstractValidator<DeleteCompliance>
    {
        public DeleteComplianceValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .GreaterThanOrEqualTo(1)
                .WithMessage(Constants.ValidationErrors.Identifier_Min_Value)
                .LessThanOrEqualTo(x => int.MaxValue)
                .WithMessage(Constants.ValidationErrors.Identifier_Max_Value);
        }
    }
}
