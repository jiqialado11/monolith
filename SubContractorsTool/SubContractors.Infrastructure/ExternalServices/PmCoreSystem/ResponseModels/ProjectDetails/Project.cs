using System;
using Newtonsoft.Json;

namespace SubContractors.Infrastructure.ExternalServices.PmCoreSystem.ResponseModels.ProjectDetails
{
    public class Project
    {
        [JsonProperty("ProjectId")]
        public int ProjectId { get; set; }
        
        [JsonProperty("ProjectDuration")]
        public string ProjectDuration { get; set; }
        
        [JsonProperty("ProjectStartDate")]
        public DateTime? ProjectStartDate { get; set; }
        
        [JsonProperty("ProjectEndDate")]
        public DateTime? ProjectEndDate { get; set; }

        [JsonProperty("ProjectEstimatedEndDate")]
        public DateTime? ProjectEstimatedEndDate { get; set; }
        
        [JsonProperty("ProjectBillDate")]
        public DateTime? ProjectBillDate { get; set; }

        [JsonProperty("ProjectStartTimesheetDate")]
        public DateTime? ProjectStartTimesheetDate { get; set; }

        [JsonProperty("ProjectEndTimesheetDate")]
        public DateTime? ProjectEndTimesheetDate { get; set; }

        [JsonProperty("ProjectIllnessPaid")]
        public int? ProjectIllnessPaid { get; set; }

        [JsonProperty("ProjectVacationsPaid")]
        public int? ProjectVacationsPaid { get; set; }

        [JsonProperty("CaseStudy")]
        public string CaseStudy { get; set; }
        
        [JsonProperty("CaseStudyComplete")]
        public bool? CaseStudyComplete { get; set; }

        [JsonProperty("StatusCode")]
        public int? StatusCode { get; set; }

        [JsonProperty("ProjectLeaderId")]
        public int? ProjectLeaderId { get; set; }

        [JsonProperty("ProjectLeaderName")]
        public string ProjectLeaderName { get; set; }

        [JsonProperty("ProjectGroupId")]
        public int? ProjectGroupId { get; set; }

        [JsonProperty("ProjectGroupName")]
        public string ProjectGroupName { get; set; }

        [JsonProperty("MetaGroupId")]
        public int? MetaGroupId { get; set; }

        [JsonProperty("MetaGroupName")]
        public string MetaGroupName { get; set; }

        [JsonProperty("BudgetGroupId")]
        public int? BudgetGroupId { get; set; }
    }
}
