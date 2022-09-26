using Newtonsoft.Json;
using System.Collections.Generic;

namespace SubContractors.Infrastructure.ExternalServices.PmCoreSystem.ResponseModels.DetailStaffList
{
    public class Response
    {
        [JsonProperty("StaffList")]
        public List<Staff> StaffList { get; set; }

        [JsonIgnore]
        public bool IsError { get; set; }

        [JsonIgnore]
        public string ErrorMessage { get; set; }
    }
}
