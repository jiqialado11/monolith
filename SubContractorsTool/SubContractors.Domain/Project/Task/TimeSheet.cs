using SubContractors.Common.EfCore;
using SubContractors.Domain.SubContractor.Staff;
using System;

namespace SubContractors.Domain.Project.Task
{
    public class TimeSheet : Entity<int>
    {
        public string Title { get; set; }
        public string WorkDescription { get; set; }
        public string Comment { get; set; }
        public Staff Staff { get; set; }
        public DateTime Date { get; set; }
        public TimeSheetStatus Status { get; set; }
        public bool IsCompleted { get; set; }
        public TimeSheet ParentTimeSheet { get; set; }
        public TimeSheetType TimeSheetType { get; set; }
        public int SpentTimeHours { get; set; }
        public Task Task { get; set; }
    }

    public enum TimeSheetType
    {
        Ordinary = 1
    }

    public enum TimeSheetStatus
    {
        Active = 1,
        InActive = 2
    }
}