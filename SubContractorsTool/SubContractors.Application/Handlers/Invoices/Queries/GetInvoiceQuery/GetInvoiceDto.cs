using SubContractors.Domain.Invoice;
using System;
using System.Collections.Generic;

namespace SubContractors.Application.Handlers.Invoices.Queries.GetInvoiceQuery
{
    public class GetInvoiceDto
    {
        public int? Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime InvoiceDate { get; set; }
        public Guid? ProjectId { get; set; }
        public GetReferralDto Referral { get; set; }
        public int? AddendumId { get; set; }
        public int? SubcontractorId { get; set; }
        public int MileStoneId { get; set; }
        public int? AgreementId { get; set; }
        public int PaymentNumber { get; set; }
        public GetBudgetDto BudgetInfo { get; set; }
        public decimal Amount { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public string Currency { get; set; }
        public string InvoiceNumber { get; set; }
        public string Comment { get; set; }
        public int InvoiceStatusId { get; set; }
        public string InvoiceStatus { get; set; }
        public bool isUseInvoiceDateForBudgetSystem { get; set; }
        public GetFileDto InvoiceFile { get; set; }
        public List<GetFileDto> SupportingDocuments { get; set; }
    }

    public class GetFileDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
    }

    public class GetBudgetDto
    {
        public Guid? Id { get; set; }
        public DateTime PaidDate { get; set; }
        public DateTime PlannedPaidDate { get; set; }
        public string BudgetInvoiceId { get; set; }
        public string BudgedRequestId { get; set; }
        public int? BudgetInvoiceStatusId { get; set; }
        public string BudgetInvoiceStatus { get; set; }
    }

    public class GetReferralDto
    {
        public int? ReferralId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? StartDate { get; set; }
    }

}
