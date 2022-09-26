using Newtonsoft.Json;

namespace SubContractors.Infrastructure.ExternalServices.BudgetSystem.ResponseModels
{
    public class RegisterInvoiceResponse
    {
        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("isError")]
        public bool IsError { get; set; }

        [JsonProperty("errorMsg")]
        public string ErrorMessage { get; set; }

        [JsonProperty("errorCode")]
        public int ErrorCode { get; set; }
    }
}
