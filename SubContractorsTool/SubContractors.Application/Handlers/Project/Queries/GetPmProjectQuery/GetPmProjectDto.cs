using System;

namespace SubContractors.Application.Handlers.Project.Queries.GetPmProjectQuery
{
    public class GetPmProjectDto
    {
        public int ProjectPmId { get; set; }
        public int ProjectGroupId { get; set; }
        public string ProjectGroup { get; set; }
        public int? ProjectManagerId { get; set; }
        public string ProjectManager { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EstimatedFinishDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public int? StatusId { get; set; }
        public string Status { get; set; }
    }
}
