using System;
using FluentValidation;
using MediatR;
using SubContractors.Application.Common;
using SubContractors.Common;

namespace SubContractors.Application.Handlers.Agreement.Commands.UpdateAgreement
{
    public class UpdateAgreement : IRequest<Result<Unit>>
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public int? LegalEntityId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? BudgetLocationId { get; set; }
        public string Conditions { get; set; }
        public int? PaymentMethodId { get; set; }
        public string AgreementUrl { get; set; }
    }

    public class UpdateAgreementValidator : AbstractValidator<UpdateAgreement>
    {
        public UpdateAgreementValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required);

            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .GreaterThanOrEqualTo(1)
                .WithMessage(Constants.ValidationErrors.Identifier_Min_Value)
                .LessThanOrEqualTo(x => int.MaxValue)
                .WithMessage(Constants.ValidationErrors.Identifier_Max_Value);

            RuleFor(x => x.LegalEntityId)
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

            RuleFor(x => x.BudgetLocationId)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .GreaterThanOrEqualTo(1)
                .WithMessage(Constants.ValidationErrors.Identifier_Min_Value)
                .LessThanOrEqualTo(x => int.MaxValue)
                .WithMessage(Constants.ValidationErrors.Identifier_Max_Value);

            RuleFor(x => x.Conditions)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required);

            RuleFor(x => x.PaymentMethodId)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .GreaterThanOrEqualTo(1)
                .WithMessage(Constants.ValidationErrors.Identifier_Min_Value)
                .LessThanOrEqualTo(x => int.MaxValue)
                .WithMessage(Constants.ValidationErrors.Identifier_Max_Value);

            RuleFor(x => x.AgreementUrl)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required);
        }
    }
}
