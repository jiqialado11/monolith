using Newtonsoft.Json;

namespace SubContractors.Infrastructure.ExternalServices.BudgetSystem.ResponseModels.BudgetDictionaries
{
    public class Currency
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
    }
}
