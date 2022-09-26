using FluentValidation;
using MediatR;
using SubContractors.Application.Common;
using SubContractors.Common;

namespace SubContractors.Application.Handlers.SubContractors.Commands.DeleteTax
{
    public class DeleteTax : IRequest<Result<Unit>>
    {
        public int? Id { get; set; }
    }

    public class DeleteTaxValidator : AbstractValidator<DeleteTax>
    {
        public DeleteTaxValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Required Field")
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .GreaterThanOrEqualTo(1)
                .WithMessage(Constants.ValidationErrors.Identifier_Min_Value)
                .LessThanOrEqualTo(x => int.MaxValue)
                .WithMessage(Constants.ValidationErrors.Identifier_Max_Value);
        }
    }
}
