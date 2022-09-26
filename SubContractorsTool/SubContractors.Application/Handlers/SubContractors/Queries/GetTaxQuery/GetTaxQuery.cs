using FluentValidation;
using MediatR;
using SubContractors.Application.Common;
using SubContractors.Common;

namespace SubContractors.Application.Handlers.SubContractors.Queries.GetTaxQuery
{
    public class GetTaxQuery : IRequest<Result<GetTaxDto>>
    {
        public int? Id { get; set; }
    }
    public class GetTaxQueryValidator : AbstractValidator<GetTaxQuery>
    {
        public GetTaxQueryValidator()
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
