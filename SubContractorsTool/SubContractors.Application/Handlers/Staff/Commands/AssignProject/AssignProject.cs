using System;
using FluentValidation;
using MediatR;
using SubContractors.Application.Common;
using SubContractors.Common;

namespace SubContractors.Application.Handlers.Staff.Commands.AssignProject
{
    public class AssignProject : IRequest<Result<Unit>>
    {
        public Guid? ProjectId { get; set; }
        public int? StaffId { get; set; }
    }

    public class AddProjectValidator : AbstractValidator<AssignProject>
    {
        public AddProjectValidator()
        {
            RuleFor(x => x.StaffId)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .GreaterThanOrEqualTo(1)
                .WithMessage(Constants.ValidationErrors.Identifier_Min_Value)
                .LessThanOrEqualTo(x => int.MaxValue)
                .WithMessage(Constants.ValidationErrors.Identifier_Max_Value);

            RuleFor(x => x.ProjectId)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required);
        }
    }
}
