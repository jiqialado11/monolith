using System.Collections.Generic;
using Newtonsoft.Json;

namespace SubContractors.Infrastructure.ExternalServices.PmCoreSystem.ResponseModels.StaffList
{
    public class Response
    {
        [JsonProperty("StaffContractorList")]
        public List<Staff> StaffContractorList { get; set; }

        [JsonIgnore]
        public bool IsError { get; set; }

        [JsonIgnore]
        public string ErrorMessage { get; set; }


        [JsonProperty("statusCode")]
        public int? StatusCode { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
