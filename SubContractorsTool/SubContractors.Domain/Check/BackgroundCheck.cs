using System;
using SubContractors.Common.EfCore;
using SubContractors.Domain.SubContractor.Staff;

namespace SubContractors.Domain.Check
{
    public class BackgroundCheck : Entity<int>
    {
        public BackgroundCheck()
        {
        }

        public BackgroundCheck(int id)
        {
            Id = id;
        }

        public int? ApproverId { get; set; }
        public Staff Approver { get; set; }
        public string Link { get; set; }
        public DateTime Date { get; set; }
        public CheckStatus CheckStatus { get; set; }
        public Staff Staff { get; set; }

        public void Create(string link, DateTime date, CheckStatus status, Staff staff)
        {
            Link = link;
            Date = date;
            CheckStatus = status;
            Staff = staff;
        }

        public void Update(string link, DateTime date, CheckStatus status, Staff staff)
        {
            Link = link;
            Date = date;
            CheckStatus = status;
            Staff = staff;
        }

        public void AssignApprover(Staff approver)
        {
            Approver = approver;
        }
    }
}
