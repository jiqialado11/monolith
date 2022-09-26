using System;
using FluentValidation;
using MediatR;
using SubContractors.Application.Common;
using SubContractors.Common;

namespace SubContractors.Application.Handlers.SubContractors.Commands.UpdateTax
{
    public class UpdateTax : IRequest<Result<Unit>>
    {
        public int? Id { get; set; }
        public int? TaxTypeId { get; set; }
        public string Name { get; set; }
        public string TaxNumber { get; set; }
        public string Url { get; set; }
        public DateTime? Date { get; set; }
    }

    public class UpdateTaxValidator : AbstractValidator<UpdateTax>
    {
        public UpdateTaxValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Required Field")
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .GreaterThanOrEqualTo(1)
                .WithMessage(Constants.ValidationErrors.Identifier_Min_Value)
                .LessThanOrEqualTo(x => int.MaxValue)
                .WithMessage(Constants.ValidationErrors.Identifier_Max_Value);

            RuleFor(x => x.TaxTypeId)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .GreaterThanOrEqualTo(1)
                .WithMessage(Constants.ValidationErrors.Identifier_Min_Value)
                .LessThanOrEqualTo(x => int.MaxValue)
                .WithMessage(Constants.ValidationErrors.Identifier_Max_Value);

            RuleFor(x => x.Name).NotEmpty().WithMessage(Constants.ValidationErrors.Field_Is_Required);

            RuleFor(x => x.TaxNumber).NotEmpty().WithMessage(Constants.ValidationErrors.Field_Is_Required);

            RuleFor(x => x.Url).NotEmpty().WithMessage(Constants.ValidationErrors.Field_Is_Required);

            RuleFor(x => x.Date).NotEmpty().WithMessage(Constants.ValidationErrors.Field_Is_Required);
        }
    }
}
