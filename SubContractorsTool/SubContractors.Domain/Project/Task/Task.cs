using SubContractors.Common.EfCore;
using SubContractors.Domain.SubContractor.Staff;
using System;
using System.Collections.Generic;

namespace SubContractors.Domain.Project.Task
{
    public class Task : Entity<int>
    {
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime EstimatedEndDate { get; set; }
        public int Priority { get; set; }
        public bool IsOnSiteTask { get; set; }
        public bool IsKarmaTimeSheetsAllowed { get; set; }
        public bool PMOrDMCanEdit { get; set; }
        public long SummaryTime { get; set; }
        public string Description { get; set; }
        public int EffortsSpent { get; set; }
        public int EstimatedEfforts { get; set; }
        public string Comment { get; set; }
        public Task ParentTask { get; set; }
        public Project Project { get; set; }
        public TaskType TaskType { get; set; }
        public TaskStatus TaskStatus { get; set; }
        public Staff ResponsiblePerson { get; set; }
        public List<Staff> Staffs { get; set; }
        public List<Note> Notes { get; set; }
        public List<TimeSheet> TimeSheets { get; set; }
    }

    public enum TaskStatus
    {
        Active = 1,
        InActive = 2
    }

    public enum TaskType
    {
        Ordinary = 1
    }
}