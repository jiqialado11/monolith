namespace SubContractors.Application.Handlers.Invoices.Queries.ExportInvoicesQuery
{
    public class ExportInvoicesDto
    {
        public string FileName { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
    }
}
