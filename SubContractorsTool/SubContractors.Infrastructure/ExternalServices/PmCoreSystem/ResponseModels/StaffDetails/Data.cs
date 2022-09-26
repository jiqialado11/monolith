using System.Collections.Generic;
using Newtonsoft.Json;

namespace SubContractors.Infrastructure.ExternalServices.PmCoreSystem.ResponseModels.StaffDetails
{
    public class Data
    {
        [JsonProperty("GeneralInfo")]
        public Staff Staff { get; set; }

        [JsonProperty("Contacts")]
        public Contacts Contacts { get; set; }

        [JsonProperty("Phones")]
        public List<Phone> Phones { get; set; }

        [JsonProperty("ExtraEmails")]
        public List<object> ExtraEmails { get; set; }
    }
}
