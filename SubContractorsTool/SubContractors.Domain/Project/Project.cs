using SubContractors.Common.EfCore;
using SubContractors.Domain.Budget;
using SubContractors.Domain.SubContractor.Staff;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using SubContractors.Domain.Agreement;

namespace SubContractors.Domain.Project
{
    public class Project : Entity<Guid>
    {
        public Project()
        {
            
        }

        public Project(Guid id)
        {
            Id = id;
        }
        public int? PmId { get; set; }
        public string Name { get; set; }
        public string StatusCode { get; set; }
        public ProjectStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime EstimatedEndDate { get; set; }
        public ProjectGroup ProjectGroup { get; set; }
        public int? DeliveryManagerId { get; set; }
        public Staff DeliveryManager { get; set; }
        public int? ProjectManagerId { get; set; }
        public Staff ProjectManager { get; set; }
        public int? InvoiceApproverId { get; set; }
        public Staff InvoiceApprover { get; set; }
        public List<SubContractor.SubContractor> SubContractors { get; set; }
        public List<Invoice.Invoice> Invoices { get; set; }
        public List<Task.Task> Tasks { get; set; }
        public List<Rate> Rates { get; set; }
        public List<Addendum> Addenda { get; set; }
        public List<Staff> Staffs { get; set; }
        public BudgetGroup BudgetGroup { get; set; }

        public void Create(int pmId, string name, DateTime? startDate, DateTime? finishDate, 
            DateTime? estimatedFinishDate)
        {
            Id = Guid.NewGuid();
            PmId = pmId;
            Name = name;

            if (startDate.HasValue)
            {
                StartDate = startDate.Value;
            }

            if (finishDate.HasValue)
            {
                EndDate = finishDate.Value;
            }

            if (estimatedFinishDate.HasValue)
            {
                EstimatedEndDate = estimatedFinishDate.Value;
            }
        }

        public void AssignStatus(int? status)
        {
            if (status.HasValue && Enum.IsDefined(typeof(ProjectStatus), status))
            {
                Status = (ProjectStatus)status.Value;
            }


        }
        public void AssignProjectGroup(ProjectGroup group)
        {
            ProjectGroup = group;
        }

        public void AssignProjectManager(Staff projectManager)
        {
            ProjectManager = projectManager;
        }

        public void AssignInvoiceApprover(Staff invoiceApprover)
        {
            InvoiceApprover = invoiceApprover;
        }

        public void DeAssignInvoiceApprover()
        {
            InvoiceApprover = null;
        }
    }

    public enum ProjectStatus
    {
        [Description("Started")]
        Started = 1,
        [Description("Not started")]
        NotStarted
    }
}