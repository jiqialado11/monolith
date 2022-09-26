using System;
using System.Collections;
using FluentValidation;
using MediatR;
using SubContractors.Application.Common;
using SubContractors.Application.Handlers.Check.Queries.GetSanctionChecksQuery;
using SubContractors.Common;

namespace SubContractors.Application.Handlers.Check.Commands.UpdateSanctionCheck
{
    public class UpdateSanctionCheck : IRequest<Result<Unit>>
    {
        public int? ParentId { get; set; }
        public int? ParentType { get; set; }
        public int? CheckId { get; set; }
        public int? ApproverId { get; set; }
        public int? CheckStatusId { get; set; }
        public DateTime? Date { get; set; }
        public string Comment { get; set; }
    }

    public class UpdateSanctionCheckValidator : AbstractValidator<UpdateSanctionCheck>
    {
        public UpdateSanctionCheckValidator()
        {
            RuleFor(x => x.ParentId).NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .GreaterThanOrEqualTo(1)
                .WithMessage(Constants.ValidationErrors.Identifier_Min_Value)
                .LessThanOrEqualTo(x => int.MaxValue)
                .WithMessage(Constants.ValidationErrors.Identifier_Max_Value);


            RuleFor(x => x.ParentType)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .Must(x => x != null && ((IList)new[] { 1, 2 }).Contains(x.Value))
                .WithMessage(Constants.ValidationErrors.Check_ParentType_Value_Range);

            RuleFor(x => x.CheckId).NotEmpty()
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
