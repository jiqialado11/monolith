using System.Collections.Generic;
using Newtonsoft.Json;

namespace SubContractors.Infrastructure.ExternalServices.BudgetSystem.ResponseModels.BudgetDictionaries
{
    public class Data
    {
        [JsonProperty("budgets")]
        public List<Budget> Budgets { get; set; }

        [JsonProperty("currencies")]
        public List<Currency> Currencies { get; set; }

        [JsonProperty("paymentMethods")]
        public List<PaymentMethod> PaymentMethods { get; set; }
    }
}
