using Newtonsoft.Json;
using System.Collections.Generic;

namespace SubContractors.Infrastructure.ExternalServices.MDPSystem.ResponseModels.LegalEntityData
{
    public class LegalEntityResponse
    {
        [JsonProperty("data")]
        public List<LegalEntityMdp> Data { get; set; }

        [JsonProperty("isError")]
        public bool IsError { get; set; }

        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; }

        [JsonProperty("errorCode")]
        public int ErrorCode { get; set; }
    }
}
