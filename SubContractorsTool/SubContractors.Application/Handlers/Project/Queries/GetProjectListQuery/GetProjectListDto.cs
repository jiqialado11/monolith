using System;

namespace SubContractors.Application.Handlers.Project.Queries.GetProjectListQuery
{
    public class GetProjectListDto
    {
        public Guid? Id { get; set; }
        public int? PmId { get; set; }
        public string Name { get; set; }
        public int? ProjectGroupId { get; set; }
        public string ProjectGroupName { get; set; }
        public int? ProjectGroupPmId { get; set; }
        public int? ProjectManagerId { get; set; }
        public string ProjectManager { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EstimatedFinishDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public int? InvoiceApproverId { get; set; }
        public string InvoiceApproverName { get; set; }
        public int? StatusId { get; set; }
        public string Status { get; set; }
    }
}