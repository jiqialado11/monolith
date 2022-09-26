using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubContractors.Infrastructure.ExternalServices.MDPSystem.RequestModels.RegisterVendor
{
    public class Vendor
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("mdpLegalEntityId")]
        public int LegalEntityId { get; set; }

        [JsonProperty("name")]
        public string FullName { get; set; }

        [JsonProperty("externalId")]
        public string ExternalId { get; set; }

        [JsonProperty("timestamp")]
        public DateTime TimeStamp { get; set; }

        [JsonProperty("isArchived")]
        public bool IsArchived { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("isEmployee")]
        public bool IsEmployee { get; set; }
    }
}
