using System;

namespace SubContractors.Application.Common.Helpers.Export
{
    public class InvoiceExportModel
    {
        public string Number { get; set; }
        public string Vendor { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string PaymentNumber { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }
    }
}
