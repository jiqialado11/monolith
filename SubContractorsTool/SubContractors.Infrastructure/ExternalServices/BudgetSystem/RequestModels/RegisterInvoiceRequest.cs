using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SubContractors.Infrastructure.ExternalServices.BudgetSystem.RequestModels
{
    public class RegisterInvoiceRequest
    {
        [JsonProperty("inputId")]
        public string InputId { get; set; }

        [JsonProperty("requests")]
        public List<BudgetsRequestInvoice> Requests { get; set; }

        [JsonProperty("invoice")]
        public RequestInvoice Invoice { get; set; }
    }

    public class BudgetsRequestInvoice
    {
        [JsonProperty("issuerDomainName")]
        public string IssuerDomainName { get; set; }

        [JsonProperty("inputId")]
        public string InputId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("budgetId")]
        public int? BudgetId { get; set; }

        [JsonProperty("projectId")]
        public int? ProjectId { get; set; }

        [JsonProperty("isUrgent")]
        public bool IsUrgent { get; set; }

        [JsonProperty("initialStatus")]
        public int? InitialStatus { get; set; }

        [JsonProperty("requestItems")]
        public List<BudgetsRequestItem> RequestItems { get; set; }        
    }

    public class RequestInvoice
    {
        [JsonProperty("inputId")]
        public string InputId { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("vendor")]
        public BudgetsVendor Vendor { get; set; }

        [JsonProperty("currencyId")]
        public int? CurrencyId { get; set; }

        [JsonProperty("currencyCode")]
        public string CurrencyCode { get; set; }

        [JsonProperty("legalEntity")]
        public BudgetsLegalEntity LegalEntity { get; set; }

        [JsonProperty("paymentMethodId")]
        public int? PaymentMethodId { get; set; }

        [JsonProperty("invoiceDate")]
        public DateTime InvoiceDate { get; set; }

        [JsonProperty("plannedForPayment")]
        public DateTime PlannedForPayment { get; set; }

        [JsonProperty("dueDate")]
        public DateTime DueDate { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("invoiceProcessingTypeId")]
        public int? InvoiceProcessingTypeId { get; set; }

        [JsonProperty("attachments")]
        public List<Attachment> Attachments { get; set; }

        [JsonProperty("varId")]
        public int? VarId { get; set; }
    }

    public class BudgetsRequestItem
    {
        [JsonProperty("inputId")]
        public string InputId { get; set; }

        [JsonProperty("issuerId")]
        public int? IssuerId { get; set; }

        [JsonProperty("requestId")]
        public int? RequestId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("periodId")]
        public int? PeriodId { get; set; }

        [JsonProperty("periodDate")]
        public DateTime PeriodDate { get; set; }

        [JsonProperty("periodDate")]
        public DateTime DueDate { get; set; }

        [JsonProperty("categoryId")]
        public int? CategoryId { get; set; }

        [JsonProperty("office")]
        public BudgetsRequestOffice Office { get; set; }

        [JsonProperty("forPersonId")]
        public int? ForPersonId { get; set; }

        [JsonProperty("milestoneId")]
        public int? MilestoneId { get; set; }

        [JsonProperty("paymentMethodId")]
        public int? PaymentMethodId { get; set; }

        [JsonProperty("isBillableToClient")]
        public bool IsBillableToClient { get; set; }

        [JsonProperty("percentBillableToClient")]
        public int? PercentBillableToClient { get; set; }

        [JsonProperty("amount")]
        public decimal? Amount { get; set; }

        [JsonProperty("currencyId")]
        public int? CurrencyId { get; set; }

        [JsonProperty("currencyCode")]
        public string CurrencyCode { get; set; }

        [JsonProperty("isReimbursement")]
        public bool IsReimbursement { get; set; }

        [JsonProperty("legalEntity")]
        public BudgetsLegalEntity LegalEntity { get; set; }

        [JsonProperty("withoutInvoice")]
        public bool WithoutInvoice { get; set; }

        [JsonProperty("paymentFulfillmentStatusId")]
        public int? PaymentFulfillmentStatusId { get; set; }

        [JsonProperty("fullyInvoiced")]
        public bool FullyInvoiced { get; set; }

        [JsonProperty("attachments")]
        public List<Attachment> Attachments { get; set; }

        [JsonProperty("invoiceItem")]
        public RequestInvoiceItem InvoiceItem { get; set; }

        [JsonProperty("paymentItem")]
        public RequestPaymentItem PaymentItem { get; set; }

        [JsonProperty("varId")]
        public RequestPaymentItem VarId { get; set; }        
    }

    public class BudgetsRequestOffice
    {
        [JsonProperty("mdpId")]
        public int? MdpId { get; set; }

        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }

    public class BudgetsVendor
    {
        [JsonProperty("mdpId")]
        public int? MdpId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("externalId")]
        public string ExternalId { get; set; }
    }

    public class BudgetsLegalEntity
    {
        [JsonProperty("mdpId")]
        public int? MdpId { get; set; }

        [JsonProperty("id")]
        public int? Id { get; set; }
    }

    public class BudgetsInvoiceItem
    {
        [JsonProperty("requestItemId")]
        public int? RequestItemId { get; set; }

        [JsonProperty("requestTimestamp")]
        public DateTime RequestTimestamp { get; set; }

        [JsonProperty("fullyInvoiced")]
        public bool FullyInvoiced { get; set; }

        [JsonProperty("netAmount")]
        public decimal? NetAmount { get; set; }

        [JsonProperty("taxRate")]
        public decimal? TaxRate { get; set; }

        [JsonProperty("taxAmount")]
        public decimal? TaxAmount { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public class Attachment
    {
        [JsonProperty("inputId")]
        public string InputId { get; set; }

        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("fileId")]
        public int? FileId { get; set; }
    }

    public class RequestInvoiceItem
    {
        [JsonProperty("inputId")]
        public string InputId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("netAmount")]
        public decimal? NetAmount { get; set; }

        [JsonProperty("taxRate")]
        public decimal? TaxRate { get; set; }

        [JsonProperty("taxAmount")]
        public decimal? TaxAmount { get; set; }

        [JsonProperty("invoiceId")]
        public int? InvoiceId { get; set; }
    }

    public class RequestPaymentItem
    {
        [JsonProperty("inputId")]
        public string InputId { get; set; }

        [JsonProperty("netAmount")]
        public decimal? Amount { get; set; }

        [JsonProperty("requestItemId")]
        public string requestItemId { get; set; }

        [JsonProperty("requestId")]
        public string RequestId { get; set; }

        [JsonProperty("paymentId")]
        public string PaymentId { get; set; }

    }
}
