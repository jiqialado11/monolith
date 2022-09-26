using System;

namespace SubContractors.Application.Handlers.Project.Queries.GetProjectQuery
{
    public class GetProjectQueryDto
    {
        public Guid Id { get; set; }
        public int PmId { get; set; }
        public string ProjectName { get; set; }
        public int? ProjectGroupId { get; set; }
        public string ProjectGroup { get; set; }
        public int? ProjectGroupPmId { get; set; }
        public int? ProjectManagerId { get; set; }
        public string ProjectManager { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EstimatedFinishDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public int ProjectStatusId { get; set; }
        public string ProjectStatus { get; set; }
    }
}
