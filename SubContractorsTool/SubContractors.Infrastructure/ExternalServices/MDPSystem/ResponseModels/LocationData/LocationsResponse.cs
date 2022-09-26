using Newtonsoft.Json;
using System.Collections.Generic;

namespace SubContractors.Infrastructure.ExternalServices.MDPSystem.ResponseModels.LocationData
{
    public class LocationsResponse
    {
        [JsonProperty("data")]
        public List<LocationMdp> Locations { get; set; }

        [JsonProperty("isError")]
        public bool IsError { get; set; }

        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; }

        [JsonProperty("errorCode")]
        public int ErrorCode { get; set; }
    }
}
