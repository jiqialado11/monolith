using Newtonsoft.Json;

namespace SubContractors.Infrastructure.ExternalServices.PmCoreSystem.ResponseModels.StaffDetails
{
    public class Contacts
    {
        [JsonProperty("MainEmail")]
        public string MainEmail { get; set; }

        [JsonProperty("SkypeForBusiness")]
        public string SkypeForBusiness { get; set; }

        [JsonProperty("Extension")]
        public int? Extension { get; set; }

        [JsonProperty("Address")]
        public string Address { get; set; }

        [JsonProperty("Skype")]
        public string Skype { get; set; }

        [JsonProperty("EmergencyPhone")]
        public string EmergencyPhone { get; set; }
    }
}
