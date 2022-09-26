using Newtonsoft.Json;

namespace SubContractors.Infrastructure.ExternalServices.BudgetSystem.ResponseModels.BudgetDictionaries
{
    public class BudgetDictionariesResponse
    {
        [JsonProperty("response_data")]
        public Data Response { get; set; }

        [JsonProperty("response_isError")]
        public bool IsError { get; set; }
    }
}
