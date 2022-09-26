using System;
using FluentValidation;
using MediatR;
using SubContractors.Application.Common;
using SubContractors.Common;

namespace SubContractors.Application.Handlers.Project.Commands.DeleteStaffProject
{
    public class DeleteStaffProject : IRequest<Result<Unit>>
    {
        public int? StaffId { get; set; }
        public Guid? ProjectId { get; set; }
    }

    public class DeleteStaffProjectValidator : AbstractValidator<DeleteStaffProject>
    {
        public DeleteStaffProjectValidator()
        {
            RuleFor(x => x.StaffId).NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .GreaterThanOrEqualTo(1)
                .WithMessage(Constants.ValidationErrors.Identifier_Min_Value)
                .LessThanOrEqualTo(x => int.MaxValue)
                .WithMessage(Constants.ValidationErrors.Identifier_Max_Value);

            RuleFor(x => x.ProjectId).NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required);
        }
    }

}
