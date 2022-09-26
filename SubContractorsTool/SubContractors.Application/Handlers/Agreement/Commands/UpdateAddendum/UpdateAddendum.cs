using System;
using System.Collections.Generic;
using FluentValidation;
using MediatR;
using SubContractors.Application.Common;
using SubContractors.Common;

namespace SubContractors.Application.Handlers.Agreement.Commands.UpdateAddendum
{
    public class UpdateAddendum : IRequest<Result<Unit>>
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public int? AgreementId { get; set; }
        public List<Guid> ProjectIds { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Comment { get; set; }
        public int? PaymentTermId { get; set; }
        public int? PaymentTermInDays { get; set; }
        public int? CurrencyId { get; set; }
        public string Url { get; set; }
        public bool? IsForNonBillableProjects { get; set; }
    }

    public class UpdateAddendumValidator : AbstractValidator<UpdateAddendum>
    {
        public UpdateAddendumValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .GreaterThanOrEqualTo(1)
                .WithMessage(Constants.ValidationErrors.Identifier_Min_Value)
                .LessThanOrEqualTo(x => int.MaxValue)
                .WithMessage(Constants.ValidationErrors.Identifier_Max_Value);

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required);

       
            RuleFor(x => x.AgreementId)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .GreaterThanOrEqualTo(1)
                .WithMessage(Constants.ValidationErrors.Identifier_Min_Value)
                .LessThanOrEqualTo(x => int.MaxValue)
                .WithMessage(Constants.ValidationErrors.Identifier_Max_Value);

            RuleFor(x => x.CurrencyId)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .GreaterThanOrEqualTo(1)
                .WithMessage(Constants.ValidationErrors.Identifier_Min_Value)
                .LessThanOrEqualTo(x => int.MaxValue)
                .WithMessage(Constants.ValidationErrors.Identifier_Max_Value);
            RuleFor(x => x.StartDate)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .Must((model, field) => field <= model.EndDate)
                .WithMessage(Constants.ValidationErrors.StartDate_less_than_EndDate);

            RuleFor(x => x.EndDate)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required);
            RuleFor(x => x.PaymentTermId)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .GreaterThanOrEqualTo(1)
                .WithMessage(Constants.ValidationErrors.Identifier_Min_Value)
                .LessThanOrEqualTo(x => int.MaxValue)
                .WithMessage(Constants.ValidationErrors.Identifier_Max_Value);

            RuleFor(x => x.ProjectIds)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required);

            RuleForEach(x => x.ProjectIds)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required);

        }
    }
}
