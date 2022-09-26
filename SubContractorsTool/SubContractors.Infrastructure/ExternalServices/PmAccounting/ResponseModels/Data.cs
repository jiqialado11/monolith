using Newtonsoft.Json;
using System.Collections.Generic;

namespace SubContractors.Infrastructure.ExternalServices.PmAccounting.ResponseModels
{
    public class Data
    {
        [JsonProperty("milestones")]
        public List<Milestone> Milestones { get; set; }
    }
}
