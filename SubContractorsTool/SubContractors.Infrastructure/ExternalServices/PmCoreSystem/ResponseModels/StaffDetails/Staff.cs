using System;
using Newtonsoft.Json;

namespace SubContractors.Infrastructure.ExternalServices.PmCoreSystem.ResponseModels.StaffDetails
{
    public class Staff
    {
        
        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("Photo")]
        public string Photo { get; set; }

        [JsonProperty("LocalName")]
        public string LocalName { get; set; }

        [JsonProperty("Active")]
        public bool? Active { get; set; }

        [JsonProperty("IsIgnoreBirthday")]
        public bool? IsIgnoreBirthday { get; set; }

        [JsonProperty("Gender")]
        public string Gender { get; set; }

        [JsonProperty("StaffFirstDate")]
        public DateTime? StaffFirstDate { get; set; }

        [JsonProperty("StaffLastDate")]
        public DateTime? StaffLastDate { get; set; }

        [JsonProperty("Department")]
        public string Department { get; set; }

        [JsonProperty("Job")]
        public string Job { get; set; }

        [JsonProperty("Location")]
        public string Location { get; set; }


        [JsonProperty("RealLocation")]
        public string RealLocation { get; set; }

        [JsonProperty("BirthDay")]
        public string BirthDay { get; set; }

        [JsonProperty("BirthDayFull")]
        public string BirthDayFull { get; set; }


        [JsonProperty("NdaSigned")]
        public bool? NdaSigned { get; set; }

        [JsonProperty("DomainLogin")]
        public string DomainLogin { get; set; }

        [JsonProperty("Staff")]
        public StaffDetails StaffDetails { get; set; }
    }
}
