using FluentValidation;
using MediatR;
using SubContractors.Application.Common;
using SubContractors.Common;

namespace SubContractors.Application.Handlers.Agreement.Queries.GetRateQuery
{
    public class GetRateQuery : IRequest<Result<GetRateDto>>
    {
        public int? RateId { get; set; }
    }

    public class GetRateQueryValidator : AbstractValidator<GetRateQuery>
    {
        public GetRateQueryValidator()
        {
            RuleFor(x => x.RateId)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .GreaterThanOrEqualTo(1)
                .WithMessage(Constants.ValidationErrors.Identifier_Min_Value)
                .LessThanOrEqualTo(x => int.MaxValue)
                .WithMessage(Constants.ValidationErrors.Identifier_Max_Value);
        }
    }
}
