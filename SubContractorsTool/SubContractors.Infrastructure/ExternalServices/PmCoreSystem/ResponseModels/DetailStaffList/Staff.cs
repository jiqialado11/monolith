using Newtonsoft.Json;
using System;

namespace SubContractors.Infrastructure.ExternalServices.PmCoreSystem.ResponseModels.DetailStaffList
{
    public class Staff
    {
        [JsonProperty("STAFF_ID")]
        public int? StaffId { get; set; }

        [JsonProperty("STAFF_NAME")]
        public string StaffName { get; set; }

        [JsonProperty("STAFF_EMAIL")]
        public string Email { get; set; }

        [JsonProperty("LOCATION_ID")]
        public int? LocationId { get; set; }

        [JsonProperty("LOCATION_NAME")]
        public string Location { get; set; }

        [JsonProperty("DEPARTMENT_ID")]
        public int? DepartmentId { get; set; }

        [JsonProperty("DEPARTMENT_NAME")]
        public string DepartmentName { get; set; }

        [JsonProperty("STAFF_FIRST_NAME")]
        public string FirstName { get; set; }

        [JsonProperty("STAFF_LAST_NAME")]
        public string LastName { get; set; }

        [JsonProperty("STAFF_EXTRA_NAME")]
        public string ExtraName { get; set; }

        [JsonProperty("IS_FAVORIT")]
        public bool IsFavorit { get; set; }

        [JsonProperty("FAVORIT_ID")]
        public int? FavoritId { get; set; }

        [JsonProperty("FAVORIT_CREATOR_ID")]
        public int? CreatorId { get; set; } 

        [JsonProperty("FAVORIT_CREATOR_NAME")]
        public string CreatorName { get; set; }

        [JsonProperty("FAVORIT_CREATE_DATE")]
        public DateTime? CreateDate { get; set; }

        [JsonProperty("REQUEST_ID")]
        public int? RequestId { get; set; }

        [JsonProperty("REQUEST_NAME")]
        public string RequestName { get; set; }

        [JsonProperty("PROJECT_GROUP_ID")]
        public int? ProjectGroupId { get; set; }

        [JsonProperty("PROJECT_GROUP_NAME")]
        public string ProjectGroupName { get; set; }

        [JsonProperty("PROJECT_ID")]
        public int? ProjectId { get; set; }

        [JsonProperty("PROJECT_NAME")]
        public string ProjectName { get; set; }

        [JsonProperty("DM_ID")]
        public int? DmId { get; set; }

        [JsonProperty("DM_NAME")]
        public string DmName { get; set;}

        [JsonProperty("PM_ID")]
        public int? PmId { get; set; }

        [JsonProperty("PM_NAME")]
        public string PmName { get; set; }

        [JsonProperty("CRM_ID")]
        public Guid? CrmId { get; set; }

        [JsonProperty("USER_FIRST_DATE")]
        public DateTime StaffStartDate { get; set; }
    }
}
