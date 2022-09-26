using System.Collections.Generic;
using FluentValidation;
using MediatR;
using SubContractors.Application.Common;
using SubContractors.Common;

namespace SubContractors.Application.Handlers.Check.Queries.GetBackgroundChecksQuery
{
    public class GetBackgroundChecksQuery : IRequest<Result<IList<GetBackgroundChecksDto>>>
    {
        public int? StaffId { get; set; }
    }

    public class GetBackgroundChecksQueryValidator : AbstractValidator<GetBackgroundChecksQuery>
    {
        public GetBackgroundChecksQueryValidator()
        {
            RuleFor(x => x.StaffId).NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .GreaterThanOrEqualTo(1)
                .WithMessage(Constants.ValidationErrors.Identifier_Min_Value)
                .LessThanOrEqualTo(x => int.MaxValue)
                .WithMessage(Constants.ValidationErrors.Identifier_Max_Value);

        }
    }
}
