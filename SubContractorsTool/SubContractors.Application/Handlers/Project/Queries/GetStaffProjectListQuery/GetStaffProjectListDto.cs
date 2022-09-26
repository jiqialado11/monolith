using System;

namespace SubContractors.Application.Handlers.Project.Queries.GetStaffProjectListQuery
{
    public class GetStaffProjectListDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public int? ProjectGroupId { get; set; }
        public string ProjectGroupName { get; set; }
        public int? ProjectManagerId { get; set; }
        public string ProjectManagerName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public int? StatusId { get; set; }
        public string Status { get; set; }
    }
}
