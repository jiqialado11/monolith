using SubContractors.Common.EfCore;
using System;
using SubContractors.Domain.SubContractor.Staff;

namespace SubContractors.Domain.Check
{
    public class SanctionCheck : Entity<int>
    {
        public SanctionCheck()
        {
        }

        public SanctionCheck(int id)
        {
            Id = id;
        }

        public int? ApproverId { get; set; }
        public Staff Approver { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public CheckStatus CheckStatus { get; set; }
        public SubContractor.SubContractor SubContractor { get; set; }
        public Staff Staff { get; set; }

        public void AssignToSubContractor(SubContractor.SubContractor subContractor)
        {
            SubContractor = subContractor;
        }

        public void AssignToStaff(Staff staff)
        {
            Staff = staff;
        }

        public void Create(string comment, DateTime date, CheckStatus status)
        {
            Comment = comment;
            Date = date;
            CheckStatus = status;
        }

        public void Update(string comment, DateTime date, CheckStatus status)
        {
            Comment = comment;
            Date = date;
            CheckStatus = status;
        }

        public void AssignApprover(Staff approver)
        {
            Approver = approver;
        }
    }
}