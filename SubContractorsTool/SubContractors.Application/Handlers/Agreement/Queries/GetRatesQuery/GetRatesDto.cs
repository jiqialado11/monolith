using System;

namespace SubContractors.Application.Handlers.Agreement.Queries.GetRatesQuery
{
    public class GetRatesDto
    {
        public int? AddendumId { get; set; }
        public int? RateId { get; set; }
        public string Title { get; set; }
        public decimal RateValue { get; set; }
        public int? RateUnitId { get; set; }
        public string RateUnit { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string Description { get; set; }
        public int? StaffId { get; set; }
        public string Staff { get; set; }
    }
}
