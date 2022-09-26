using Newtonsoft.Json;

namespace SubContractors.Infrastructure.ExternalServices.PmAccounting.ResponseModels
{
    public class PmAccountingResponse
    {
        [JsonProperty("data")]
        public Data Data { get; set; }

        [JsonProperty("isError")]
        public bool IsError { get; set; }

        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; }

        [JsonProperty("errorCode")]
        public int ErrorCode { get; set; }
    }
}
