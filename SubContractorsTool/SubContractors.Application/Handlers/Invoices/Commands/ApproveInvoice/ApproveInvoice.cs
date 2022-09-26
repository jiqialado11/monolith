using FluentValidation;
using MediatR;
using SubContractors.Application.Common;
using SubContractors.Application.Handlers.Invoices.Commands.Models;
using SubContractors.Common;
using SubContractors.Domain.Invoice;

namespace SubContractors.Application.Handlers.Invoices.Commands.ApproveInvoice
{
    public class ApproveInvoice : IRequest<Result<Unit>>
    {
        public int? Id { get; set; }
        public int? InvoiceStatusId { get; set; }
        public string InvoiceNumber { get; set; }
        public CreateMilestone Milestone { get; set; }
    }

    public class ApproveInvoiceValidator : AbstractValidator<ApproveInvoice>
    {
        public ApproveInvoiceValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .GreaterThanOrEqualTo(1)
                .WithMessage(Constants.ValidationErrors.Identifier_Min_Value)
                .LessThanOrEqualTo(x => int.MaxValue)
                .WithMessage(Constants.ValidationErrors.Identifier_Max_Value);

            RuleFor(x => x.InvoiceStatusId)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .Must(x => x == (int)InvoiceStatus.Approved)
                .WithMessage(Constants.ValidationErrors.Identifier_Invoice_Status_Approve_Value);

            RuleFor(x => x.InvoiceNumber)
                .NotNull()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required);

            When(x => !(x.Milestone is null), () => {
                RuleFor(x => x.Milestone.PmId)
                    .NotEmpty()
                    .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                    .GreaterThanOrEqualTo(1)
                    .WithMessage(Constants.ValidationErrors.Identifier_Min_Value)
                    .LessThanOrEqualTo(x => int.MaxValue)
                    .WithMessage(Constants.ValidationErrors.Identifier_Max_Value);

                RuleFor(x => x.Milestone.Name)
                    .NotEmpty()
                    .WithMessage(Constants.ValidationErrors.Field_Is_Required);

                RuleFor(x => x.Milestone.ToDate)
                    .NotEmpty()
                    .WithMessage(Constants.ValidationErrors.Field_Is_Required);
            });
        }
    }

    
}
