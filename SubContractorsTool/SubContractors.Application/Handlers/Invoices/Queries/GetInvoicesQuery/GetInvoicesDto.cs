using System;

namespace SubContractors.Application.Handlers.Invoices.Queries.GetInvoicesQuery
{
    public class GetInvoicesDto
    {
        public int Id { get; set; }
        public int? ReferralId { get; set; }
        public int? AddendumId { get; set; }
        public int? MileStoneId { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid? BudgedInfoId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Project { get; set; }
        public decimal Amount { get; set; }
        public decimal TaxAmount { get; set; }
        public string Currency { get; set; }
        public string InvoiceNumber { get; set; }
        public int InvoiceStatusId { get; set; }
        public string InvoiceStatus { get; set; }
        public bool IsUseInvoiceDateForBudgetSystem { get; set; }
    }
}