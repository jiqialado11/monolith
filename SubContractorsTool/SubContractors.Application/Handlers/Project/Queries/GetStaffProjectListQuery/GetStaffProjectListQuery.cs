using System.Collections.Generic;
using FluentValidation;
using MediatR;
using SubContractors.Application.Common;
using SubContractors.Common;

namespace SubContractors.Application.Handlers.Project.Queries.GetStaffProjectListQuery
{
    public class GetStaffProjectListQuery : IRequest<Result<IList<GetStaffProjectListDto>>>
    {
        public int? StaffId { get; set; }
    }

    public class GetStaffProjectListQueryValidator : AbstractValidator<GetStaffProjectListQuery>
    {
        public GetStaffProjectListQueryValidator()
        {
            RuleFor(x => x.StaffId)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .GreaterThanOrEqualTo(1)
                .WithMessage(Constants.ValidationErrors.Identifier_Min_Value)
                .LessThanOrEqualTo(x => int.MaxValue)
                .WithMessage(Constants.ValidationErrors.Identifier_Max_Value);
        }
    }
}
