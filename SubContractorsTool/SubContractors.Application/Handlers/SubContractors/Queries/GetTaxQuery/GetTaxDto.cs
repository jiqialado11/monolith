using System;

namespace SubContractors.Application.Handlers.SubContractors.Queries.GetTaxQuery
{
    public class GetTaxDto
    {
        public int? Id { get; set; }
        public int? SubContractorId { get; set; }
        public int? TaxTypeId { get; set; }
        public string Name { get; set; }
        public string TaxNumber { get; set; }
        public string Url { get; set; }
        public DateTime? Date { get; set; }
    }
}
