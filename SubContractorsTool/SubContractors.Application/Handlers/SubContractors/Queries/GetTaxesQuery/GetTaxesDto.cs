using System;

namespace SubContractors.Application.Handlers.SubContractors.Queries.GetTaxesQuery
{
    public class GetTaxesDto
    {
        public int? Id { get; set; }
        public int? SubContractorId { get; set; }
        public int? TaxTypeId { get; set; }
        public string TaxType { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Url { get; set; }
        public DateTime? Date { get; set; }
    }
}
