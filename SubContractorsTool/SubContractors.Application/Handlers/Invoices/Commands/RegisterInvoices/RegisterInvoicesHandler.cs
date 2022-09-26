using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Domain.Invoice;
using SubContractors.Infrastructure.ExternalServices.BudgetSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SubContractors.Infrastructure.ExternalServices.BudgetSystem.RequestModels;

namespace SubContractors.Application.Handlers.Invoices.Commands.RegisterInvoices
{
    public class RegisterInvoicesHandler : IRequestHandler<RegisterInvoices, Result<Unit>>
    {
        private readonly ISqlRepository<Invoice, int> _invoiceSqlRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBudgetsService _budgetService;
        public RegisterInvoicesHandler(ISqlRepository<Invoice, int> invoiceSqlRepository,
                                       IUnitOfWork unitOfWork,
                                       IBudgetsService budgetService)
        {
            _invoiceSqlRepository = invoiceSqlRepository;
            _unitOfWork = unitOfWork;
            _budgetService = budgetService;
        }

        public async Task<Result<Unit>> Handle(RegisterInvoices request, CancellationToken cancellationToken)
        {
            var invoices = await _invoiceSqlRepository.FindAsync(x => x.InvoiceNumber == request.InvoiceNumber, 
                                                                new string [] {nameof(Invoice.MileStone)});
            if (invoices == null)
            {
                return Result.NotFound($"Invoice wasn't found in database by invoice number {request.InvoiceNumber}");
            }

            var count = 0;
            foreach (var invoice in invoices)
            {
                if (invoice.InvoiceStatus == InvoiceStatus.Approved)
                {
                    count++;
                }
            }

            if (count != invoices.Count())
            {
                await _unitOfWork.SaveAsync();
                return Result.Accepted();
            }

            var requestId = new Guid();
            var invoiceRequests = new List<RegisterInvoiceRequest>();

            foreach (var invoice in invoices)
            {
                var description = $"Payment to contractor {invoice.SubContractor.Name} for work from {invoice.StartDate} to" +
                                  $" {invoice.EndDate}, project {invoice.Project.Name}";
                var office = new BudgetsRequestOffice
                {
                    Id = invoice.SubContractor.Location.Id,
                    Code = invoice.SubContractor.Location.Code,
                    MdpId = invoice.SubContractor.Location.Id
                };

                var legalEntity = new BudgetsLegalEntity 
                {
                    Id = invoice.Addendum.Agreement.LegalEntity.Id,
                    MdpId = null,
                };

                var invoiceItem = new RequestInvoiceItem
                {
                    InputId = requestId.ToString(),
                    Description = description,
                    NetAmount = invoice.Amount,
                    TaxRate = invoice.TaxRate,
                    TaxAmount = invoice.TaxAmount,
                    InvoiceId = invoice.Id
                };

                var paymentItem = new RequestPaymentItem
                {
                    InputId = requestId.ToString(),
                    Amount = null,
                    RequestId = null,
                    requestItemId = null,
                    PaymentId = null
                };

                var dueDate = invoice.InvoiceDate.AddDays(invoice.Addendum.PaymentTermInDays); 

                var budgetsRequest = new RegisterInvoiceRequest
                {
                    InputId = requestId.ToString(),
                    Requests = new List<BudgetsRequestInvoice>{new BudgetsRequestInvoice
                    {
                        InputId = requestId.ToString(),
                        Description = description,
                        BudgetId = 1,
                        ProjectId = invoice.Project.PmId,
                        IssuerDomainName = "91108",
                        IsUrgent = false,
                        InitialStatus = 3,
                        RequestItems = new List<BudgetsRequestItem> { new BudgetsRequestItem
                        {
                            InputId = requestId.ToString(),
                            IssuerId = null,
                            RequestId = 1,
                            Description = description,
                            PeriodId = null,
                            PeriodDate = invoice.InvoiceDate,
                            CategoryId = 2,
                            Office = office,
                            ForPersonId = null,
                            MilestoneId = invoice.MileStone.PmId,
                            PaymentMethodId = invoice.Addendum.Agreement.PaymentMethod.Id,
                            IsBillableToClient = false,
                            PercentBillableToClient = null,
                            Amount = invoice.Amount,
                            CurrencyId = invoice.Addendum.Currency.Id,
                            CurrencyCode = invoice.Addendum.Currency.Code,
                            IsReimbursement = false,
                            LegalEntity = legalEntity,
                            WithoutInvoice = false,
                            PaymentFulfillmentStatusId = null,
                            FullyInvoiced = true,
                            Attachments = new List<Attachment>(),
                            InvoiceItem = invoiceItem,
                            PaymentItem = paymentItem,
                            VarId = null
                        }},
                        
                    }},

                    Invoice = new RequestInvoice
                    {
                        InputId = requestId.ToString(),
                        Number = invoice.InvoiceNumber,
                        Vendor = new BudgetsVendor
                        {
                            MdpId = invoice.SubContractor.Id,
                            Name = invoice.SubContractor.Name,
                            ExternalId = invoice.SubContractor.Id.ToString()
                        },
                        CurrencyId = invoice.Addendum.Currency.Id,
                        CurrencyCode = invoice.Addendum.Currency.Code,
                        LegalEntity = legalEntity,
                        PaymentMethodId = invoice.Addendum.Agreement.PaymentMethod.Id,
                        InvoiceDate = invoice.InvoiceDate,
                        DueDate = dueDate,
                        Description = description,
                        InvoiceProcessingTypeId = null,
                    }
                };

                invoiceRequests.Add(budgetsRequest);                
            }
            var result = await _budgetService.RegisterInvoicesAsync(invoiceRequests);
            //await _unitOfWork.SaveAsync();
            return Result.Accepted();            
        }
    }
}
