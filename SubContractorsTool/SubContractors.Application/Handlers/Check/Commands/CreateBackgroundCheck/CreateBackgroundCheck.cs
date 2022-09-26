using System;
using FluentValidation;
using MediatR;
using SubContractors.Application.Common;
using SubContractors.Common;

namespace SubContractors.Application.Handlers.Check.Commands.CreateBackgroundCheck
{
    public class CreateBackgroundCheck : IRequest<Result<int>>
    {
        public int? StaffId { get; set; }
        public int? ApproverId { get; set; }
        public int? CheckStatusId { get; set; }
        public DateTime? Date { get; set; }
        public string Link { get; set; }
    }

    public class CreateBackgroundCheckValidator : AbstractValidator<CreateBackgroundCheck>
    {
        public CreateBackgroundCheckValidator()
        {
            RuleFor(x => x.StaffId)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .GreaterThanOrEqualTo(1)
                .WithMessage(Constants.ValidationErrors.Identifier_Min_Value)
                .LessThanOrEqualTo(x => int.MaxValue)
                .WithMessage(Constants.ValidationErrors.Identifier_Max_Value);

            RuleFor(x => x.ApproverId).NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .GreaterThanOrEqualTo(1)
                .WithMessage(Constants.ValidationErrors.Identifier_Min_Value)
                .LessThanOrEqualTo(x => int.MaxValue)
                .WithMessage(Constants.ValidationErrors.Identifier_Max_Value);

            RuleFor(x => x.CheckStatusId)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .ExclusiveBetween(0, 3)
                .WithMessage(Constants.ValidationErrors.Check_Status_Value_Range);

            RuleFor(x => x.Date).NotEmpty().WithMessage(Constants.ValidationErrors.Field_Is_Required);
        }
    }
}
