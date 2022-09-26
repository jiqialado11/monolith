using System.Collections.Generic;
using FluentValidation;
using MediatR;
using SubContractors.Application.Common;
using SubContractors.Common;

namespace SubContractors.Application.Handlers.Agreement.Queries.GetAgreementsQuery
{
    public class GetAgreementsQuery : IRequest<Result<IList<GetAgreementsDto>>>
    {
        public int? SubContractorId { get; set; }
    }

    public class GetAgreementsQueryValidator : AbstractValidator<GetAgreementsQuery>
    {
        public GetAgreementsQueryValidator()
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