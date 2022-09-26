using FluentValidation;
using MediatR;
using SubContractors.Application.Common;
using SubContractors.Application.Handlers.Compliance.Queries.GetComplianceListQuery;
using SubContractors.Common;

namespace SubContractors.Application.Handlers.Compliance.Queries.GetComplianceQuery
{
    public class GetComplianceQuery : IRequest<Result<GetComplianceDto>>
    {
        public int? Id { get; set; }
    }

    public class GetComplianceQueryValidator : AbstractValidator<GetComplianceQuery>
    {
        public GetComplianceQueryValidator()
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
