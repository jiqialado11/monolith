using System.Collections.Generic;
using FluentValidation;
using MediatR;
using SubContractors.Application.Common;
using SubContractors.Common;

namespace SubContractors.Application.Handlers.Project.Queries.GetSubContractorsProjectListByStaffQuery
{
    public class GetSubContractorsProjectListByStaffQuery: IRequest<Result<IList<GetSubContractorsProjectListByStaffDto>>>
    {
        public int? StaffId { get; set; }
    }

    public class GetSubContractorsProjectListByStaffQueryValidator : AbstractValidator<GetSubContractorsProjectListByStaffQuery>
    {
        public GetSubContractorsProjectListByStaffQueryValidator()
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
