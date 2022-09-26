using FluentValidation;
using MediatR;
using SubContractors.Common;

namespace SubContractors.Application.Handlers.Invoices.Commands.RegisterInvoices
{
    public class RegisterInvoices : IRequest<Result<Unit>>
    {
        public string InvoiceNumber { get; set; }
    }

    public class RegisterInvoiceValidator : AbstractValidator<RegisterInvoices>
    {

    }
}
