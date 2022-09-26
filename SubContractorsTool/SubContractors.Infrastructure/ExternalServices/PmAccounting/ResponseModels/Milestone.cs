using Newtonsoft.Json;

namespace SubContractors.Infrastructure.ExternalServices.PmAccounting.ResponseModels
{
    public class Milestone
    {
        [JsonProperty("milestoneId")]
        public int MilestoneId { get; set; }

        [JsonProperty("milestoneName")]
        public string MilestoneName { get; set;}

        [JsonProperty("milestoneAmountUsd")]
        public double Amount { get; set; }

        [JsonProperty("milestoneTypeId")]
        public int MilestoneTypeId { get; set; }

        [JsonProperty("milestoneToDate")]
        public string MilestoneToDate { get; set; }

        [JsonProperty("invoicePdfLink")]
        public string InvoicePdfLink { get; set; }
    }

}
