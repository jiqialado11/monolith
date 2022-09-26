using System.Collections.Generic;
using FluentValidation;
using MediatR;
using SubContractors.Application.Common;
using SubContractors.Common;

namespace SubContractors.Application.Handlers.SubContractors.Queries.GetTaxesQuery
{
    public class GetTaxesQuery : IRequest<Result<IList<GetTaxesDto>>>
    {
        public int? SubContractorId { get; set; }
    }

    public class GetTaxesQueryValidator : AbstractValidator<GetTaxesQuery>
    {
        public GetTaxesQueryValidator()
        {
            RuleFor(x => x.SubContractorId)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .GreaterThanOrEqualTo(1)
                .WithMessage(Constants.ValidationErrors.Identifier_Min_Value)
                .LessThanOrEqualTo(x => int.MaxValue)
                .WithMessage(Constants.ValidationErrors.Identifier_Max_Value);
        }
    }
}
