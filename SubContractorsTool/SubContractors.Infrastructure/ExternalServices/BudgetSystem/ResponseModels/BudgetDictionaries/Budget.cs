using Newtonsoft.Json;

namespace SubContractors.Infrastructure.ExternalServices.BudgetSystem.ResponseModels.BudgetDictionaries
{
    public class Budget
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("isInactive")]
        public bool IsInactive { get; set; }
    }
}
