using System.Collections;
using System.Collections.Generic;
using FluentValidation;
using MediatR;
using SubContractors.Application.Common;
using SubContractors.Common;

namespace SubContractors.Application.Handlers.Check.Queries.GetSanctionChecksQuery
{
    public class GetSanctionChecksQuery : IRequest<Result<IList<GetSanctionChecksDto>>>
    {
        public int? ParentId { get; set; }

        public int? ParentType { get; set; }
    }

    public enum ParentType
    {
        SubContractor = 1,
        Staff
    }

    public class GetSanctionChecksQueryValidator : AbstractValidator<GetSanctionChecksQuery>
    {
        public GetSanctionChecksQueryValidator()
        {
            RuleFor(x => x.ParentType)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .Must(x => x != null && ((IList)new[] { 1, 2 }).Contains(x.Value))
                .WithMessage(Constants.ValidationErrors.Check_ParentType_Value_Range);

            RuleFor(x => x.ParentId).NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .GreaterThanOrEqualTo(1)
                .WithMessage(Constants.ValidationErrors.Identifier_Min_Value)
                .LessThanOrEqualTo(x => int.MaxValue)
                .WithMessage(Constants.ValidationErrors.Identifier_Max_Value);

        }
    }
}
