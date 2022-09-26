using Newtonsoft.Json;

namespace SubContractors.Infrastructure.ExternalServices.MDPSystem.RequestModels.RegisterVendor
{
    public class PostVendorResponse
    {
        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }

        [JsonProperty("isError")]
        public bool IsError { get; set; }

        [JsonProperty("errorCode")]
        public int ErrorCode { get; set; }

        [JsonProperty("errorMessage")]
        public string Message { get; set; }
    }
}
