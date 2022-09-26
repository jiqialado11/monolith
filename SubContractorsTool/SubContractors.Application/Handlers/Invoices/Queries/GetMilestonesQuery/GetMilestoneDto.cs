using System;

namespace SubContractors.Application.Handlers.Invoices.Queries.GetMilestonesQuery
{
    public class GetMilestoneDto
    {
        public int? PmId { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public DateTime ToDate { get; set; }
    }
}
