using System;

namespace SubContractors.Application.Handlers.Invoices.Queries.GetAllInvoicesPagedQuery
{
    public class GetAllInvoicesPagedDto
    {
        public int Id { get; set; }
        public int? SubcontractorId { get; set; }
        public int? PaymentNumber { get; set; }
        public int? ReferralId { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime PaidDate { get; set; }
        public DateTime PlannedPaidDate { get; set; }
        public string SubContractorName { get; set; }
        public string SubContractorType { get; set; }
        public int? SubContractorTypeId { get; set; }
        public string InvoiceNumber { get; set; }
        public int InvoiceStatusId { get; set; }
        public string InvoiceStatus { get; set; }
        public string LegalEntity { get; set; }
        public string AccountManagerName { get; set; }
        public string ApproverName { get; set; }
        public Guid ProjectId { get; set; }
        public bool IsUseInvoiceDateForBudgetSystem { get; set; }
    }

    public class GetAccountManagerDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
    }

    public class GetApproverDto
    {
        public int? Id { get; set;}
        public string Name { get; set; }
    }
}
