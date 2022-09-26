using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SubContractors.Infrastructure.ExternalServices.MDPSystem.ResponseModels.ContractorData
{
    public class ContractorMdp
    {
        [JsonProperty("versionId")]
        public int VersionId { get; set; }

        [JsonProperty("entityId")]
        public int Id { get; set; }

        [JsonProperty("legalEntityId")]
        public int legalEntityId { get; set; }

        [JsonProperty("startDate")]
        public DateTime StartDate { get; set; }

        [JsonProperty("finishDate")]
        public DateTime FinishDate { get; set; }

        [JsonProperty("timestamp")]
        public DateTime TimeStamp { get; set; }

        [JsonProperty("entityIsDeleted")]
        public bool EntityIsDeleted { get; set; }

        [JsonProperty("versionIsDeleted")]
        public bool VersionIsDeleted { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("startDocumentDatetime")]
        public DateTime StartDocumentDatetime { get; set; }

        [JsonProperty("finishDocumentDatetime")]
        public DateTime FinishDocumentDatetime { get; set; }

        [JsonProperty("documentType")]
        public string DocumentType { get; set; }

        [JsonProperty("originalDocumentUrl")]
        public string OriginalDocumentUrl { get; set; }

        [JsonProperty("vendorId")]
        public int VendorId { get; set; }

        [JsonProperty("tagList")]
        public List<string> TagList { get; set; }
    }
}
