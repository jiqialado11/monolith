using Newtonsoft.Json;

namespace SubContractors.Infrastructure.ExternalServices.PmCoreSystem.ResponseModels.ProjectList
{
    public class Project
    {
        [JsonProperty("projectId")]
        public int ProjectId { get; set; }
        
        [JsonProperty("projectName")]
        public string ProjectName { get; set; }
    }
}
