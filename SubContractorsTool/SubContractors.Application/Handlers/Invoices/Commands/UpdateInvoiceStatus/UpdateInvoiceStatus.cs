using FluentValidation;
using MediatR;
using SubContractors.Application.Common;
using SubContractors.Application.Handlers.Invoices.Commands.Models;
using SubContractors.Common;
using SubContractors.Common.Redis;
using SubContractors.Domain.Invoice;
using System.Collections.Generic;

namespace SubContractors.Application.Handlers.Invoices.Commands.UpdateInvoiceStatus
{
    public class UpdateInvoiceStatus : IRequest<Result<Unit>>, IInvalidateCacheRequest
    {
        public int? InvoiceId { get; set; }
        public int? StatusId { get; set; }
        public string InvoiceNumber { get; set; }
        public CreateMilestone Milestone { get; set; }

        public List<string> GetDomainsIdentifiers()
        {
            return new List<string> {
                                      typeof(Invoice).FullName,
                                     };
        }
    }

    public class UpdateInvoiceStatusValidator : AbstractValidator<UpdateInvoiceStatus>
    {
        public UpdateInvoiceStatusValidator()
        {
            RuleFor(x => x.InvoiceId)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .GreaterThanOrEqualTo(1)
                .WithMessage(Constants.ValidationErrors.Identifier_Min_Value)
                .LessThanOrEqualTo(x => int.MaxValue)
                .WithMessage(Constants.ValidationErrors.Identifier_Max_Value);

            RuleFor(x => x.StatusId)
                .NotEmpty()
                .WithMessage(Constants.ValidationErrors.Field_Is_Required)
                .GreaterThanOrEqualTo((int)InvoiceStatus.New)
                .WithMessage(Constants.ValidationErrors.Invoice_Status_Value_Range)
                .LessThanOrEqualTo(x => (int)InvoiceStatus.Rejected)
                .WithMessage(Constants.ValidationErrors.Invoice_Status_Value_Range);
        }
    }
}
