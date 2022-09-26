using Newtonsoft.Json;

namespace SubContractors.Infrastructure.ExternalServices.PmCoreSystem.ResponseModels.StaffList
{
    public class Staff
    {

        [JsonProperty("StaffId")]
        public int StaffId { get; set; }
        
        [JsonProperty("DepartmentId")]
        public int DepartmentId { get; set; }
        
        [JsonProperty("FirstName")]
        public string FirstName { get; set; }
        
        [JsonProperty("LastName")]
        public string LastName { get; set; }
    }
}
