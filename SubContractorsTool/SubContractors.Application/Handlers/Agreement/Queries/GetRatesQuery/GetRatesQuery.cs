using System.Collections.Generic;
using FluentValidation;
using MediatR;
using SubContractors.Application.Common;
using SubContractors.Common;

namespace SubContractors.Application.Handlers.Agreement.Queries.GetRatesQuery
{
    public class GetRatesQuery : IRequest<Result<IList<GetRatesDto>>>
    {
        public int? AddendumId { get; set; }
    }
    public class GetRatesQueryValidator : AbstractValidator<GetRatesQuery>
    {
        public GetRatesQueryValidator()
        {
            RuleFor(x => x.AddendumId)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .GreaterThanOrEqualTo(1)
                .WithMessage(Constants.ValidationErrors.Identifier_Min_Value)
                .LessThanOrEqualTo(x => int.MaxValue)
                .WithMessage(Constants.ValidationErrors.Identifier_Max_Value);
        }
    }

}
