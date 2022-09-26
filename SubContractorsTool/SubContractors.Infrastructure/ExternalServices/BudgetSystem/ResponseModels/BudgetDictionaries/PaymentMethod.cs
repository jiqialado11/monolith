using Newtonsoft.Json;

namespace SubContractors.Infrastructure.ExternalServices.BudgetSystem.ResponseModels.BudgetDictionaries
{
    public class PaymentMethod
    {
        [JsonProperty("isInactive")]
        public bool IsInactive { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
