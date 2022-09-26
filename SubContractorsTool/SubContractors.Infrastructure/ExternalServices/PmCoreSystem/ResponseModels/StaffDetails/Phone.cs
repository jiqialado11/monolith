using Newtonsoft.Json;

namespace SubContractors.Infrastructure.ExternalServices.PmCoreSystem.ResponseModels.StaffDetails
{
    public class Phone
    {
        [JsonProperty("Phone")]
        public string PhoneNumber { get; set; }
        
        [JsonProperty("PhoneType")]
        public PhoneType PhoneType { get; set; }

    }

    public class PhoneType
    {
        [JsonProperty("Id")]
        public int? PhoneNumber { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }
    }
}
