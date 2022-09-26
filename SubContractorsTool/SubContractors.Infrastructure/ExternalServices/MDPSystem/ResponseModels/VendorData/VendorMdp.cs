using Newtonsoft.Json;
using System;

namespace SubContractors.Infrastructure.ExternalServices.MDPSystem.ResponseModels.VendorData
{
    public class VendorMdp
    {
        [JsonProperty("legalEntityId")]
        public int LegalEntityId { get; set; }

        [JsonProperty("versionId")]
        public int VersionId { get; set; }

        [JsonProperty("entityId")]
        public int EntityId { get; set; }

        [JsonProperty("externalId")]
        public string externalId { get; set; }

        [JsonProperty("englishName")]
        public string EnglishName { get; set; }

        [JsonProperty("isArchived")]
        public bool IsArchived { get; set; }

        [JsonProperty("entityIsDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("versionIsDeleted")]
        public bool VersionIsDeleted { get; set; }

        [JsonProperty("startDate")]
        public DateTime StartDate { get; set; }

        [JsonProperty("finishDate")]
        public DateTime FinishDate { get; set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
