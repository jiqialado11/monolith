using FluentValidation;
using MediatR;
using SubContractors.Application.Common;
using SubContractors.Common;

namespace SubContractors.Application.Handlers.Staff.Queries.GetStaffQuery
{
    public class GetStaffQuery : IRequest<Result<GetStaffDto>>
    {
        public int? Id { get; set; }
    }

    public class GetStaffQueryValidator : AbstractValidator<GetStaffQuery>
    {
        public GetStaffQueryValidator()
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
