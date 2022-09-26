using System.Collections.Generic;
using Newtonsoft.Json;

namespace SubContractors.Infrastructure.ExternalServices.PmCoreSystem.ResponseModels.ProjectList
{
    public class Response
    {
        public List<Project> Data { get; set; }

        [JsonIgnore]
        public bool IsError { get; set; }

        [JsonIgnore]
        public string ErrorMessage { get; set; }
    }
}
