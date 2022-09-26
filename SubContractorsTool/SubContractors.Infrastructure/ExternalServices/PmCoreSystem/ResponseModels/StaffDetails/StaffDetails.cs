using Newtonsoft.Json;

namespace SubContractors.Infrastructure.ExternalServices.PmCoreSystem.ResponseModels.StaffDetails
{
    public class StaffDetails
    {
        [JsonProperty("Id")]
        public int? Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }
    }
}
