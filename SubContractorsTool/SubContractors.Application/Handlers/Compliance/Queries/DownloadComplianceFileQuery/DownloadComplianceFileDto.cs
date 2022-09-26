namespace SubContractors.Application.Handlers.Compliance.Queries.DownloadComplianceFileQuery
{
    public class DownloadComplianceFileDto
    {
        public string FileName { get; set; }
        public byte [] Content { get; set; }
        public string ContentType { get; set; }
    }
}
