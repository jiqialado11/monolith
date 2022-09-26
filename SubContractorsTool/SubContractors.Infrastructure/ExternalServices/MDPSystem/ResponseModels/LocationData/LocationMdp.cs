using Newtonsoft.Json;
using System;

namespace SubContractors.Infrastructure.ExternalServices.MDPSystem.ResponseModels.LocationData
{
    public class LocationMdp
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("englishName")]
        public string EnglishName { get; set; }

        [JsonProperty("leaderPMID")]
        public int? LeaderPMID { get; set; }

        [JsonProperty("countryId")]
        public int? CountryId { get; set; }

        [JsonProperty("isArchived")]
        public bool IsArchived { get; set; }

        [JsonProperty("defaultCurrencyCode")]
        public string DefaultCurrencyCode { get; set; }

        [JsonProperty("timezoneName")]
        public string TimezoneName { get; set; }

        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        [JsonProperty("isProduction")]
        public string IsProduction { get; set; }

        [JsonProperty("isOnsite")]
        public bool IsOnsite { get; set; }

        [JsonProperty("versionId")]
        public int VersionId { get; set; }

        [JsonProperty("entityId")]
        public int EntityId { get; set; }

        [JsonProperty("startDate")]
        public DateTime StartDate { get; set; }

        [JsonProperty("finishDate")]
        public DateTime FinishDate { get; set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("entityIsDeleted")]
        public bool EntityIsDeleted { get; set; }

        [JsonProperty("versionIsDeleted")]
        public bool VersionIsDeleted { get; set; }
    }
}
