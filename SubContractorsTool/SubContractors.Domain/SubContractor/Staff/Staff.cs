using SubContractors.Common.EfCore;
using SubContractors.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using SubContractors.Domain.Agreement;
using SubContractors.Domain.Check;

namespace SubContractors.Domain.SubContractor.Staff
{
    public class Staff : Entity<int>
    {
        public Staff()
        {

        }

        public Staff(int id)
        {
            Id = id;
        }
        public int PmId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Skype { get; set; }
        public string Position { get; set; }
        public DateTime? StartDate { get; set; } 
        public DateTime CannotLoginBefore { get; set; }
        public DateTime CannotLoginAfter { get; set; }
        public string Qualifications { get; set; }
        public Location Location { get; set; }
        public string RealLocation { get; set; }
        public string CellPhone { get; set; }
        public bool IsNdaSigned { get; set; }
        public string DepartmentName { get; set; }
        public string DomainLogin { get; set; }
        public StaffStatus Status { get; set; }
        public List<Invoice.Invoice> Invoices { get; set; }
        public List<Addendum> Addenda { get; set; }
        public List<SubContractor> SubContractors { get; set; }
        public List<Project.Project> Projects { get; set; }
        public Location BudgetOffice { get; set; }
        public List<Rate> Rates { get; set; }
        public List<BackgroundCheck> BackgroundChecks { get; set; }
        public List<SanctionCheck> SanctionChecks { get; set; }
        public void Create(int pmId, string firstName, string lastName,
            string email, string skype, string position, DateTime? cannotLoginBefore,
            DateTime? cannotLoginAfter, string qualifications, string cellPhone, bool? isNdaSigned
            , string departmentName, string domainLogin, string realLocation, DateTime? startDate)
        {
            PmId = pmId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            DepartmentName = departmentName;
            RealLocation = realLocation;

            if (!string.IsNullOrEmpty(qualifications))
            {
                Qualifications = qualifications;
            }

            if (!string.IsNullOrEmpty(position))
            {
                Position = position;
            }

            if (!string.IsNullOrEmpty(domainLogin))
            {
                DomainLogin = domainLogin;
            }

            if (!string.IsNullOrEmpty(skype))
            {
                Skype = skype;
            }

            if (!string.IsNullOrEmpty(cellPhone))
            {
                CellPhone = cellPhone;
            }

            if (isNdaSigned.HasValue)
            {
                IsNdaSigned = isNdaSigned.Value;
            }

            if (cannotLoginAfter.HasValue)
            {
                CannotLoginAfter = cannotLoginAfter.Value;
            }

            if (cannotLoginBefore.HasValue)
            {
                CannotLoginBefore = cannotLoginBefore.Value;
            }

            if (cannotLoginAfter != null && cannotLoginBefore != null && cannotLoginBefore.Value <= cannotLoginAfter.Value)
            {
                Status = cannotLoginAfter.Value.Date < DateTime.Now.Date ? StaffStatus.InActive : StaffStatus.Active;
            }
            else
            {
                Status = StaffStatus.InActive;
            }

            if (startDate.HasValue)
            {
                StartDate = startDate.Value;
            }
        }

        public void Update(
            string email, string skype, DateTime? cannotLoginBefore,
            DateTime? cannotLoginAfter, string cellPhone, string departmentName, string domainLogin, DateTime? startDate, string realLocation)
        {
            Email = email;            
            DepartmentName = departmentName;

            if (!string.IsNullOrEmpty(realLocation))
            {
                RealLocation = realLocation;
            }

            if (!string.IsNullOrEmpty(domainLogin))
            {
                DomainLogin = domainLogin;
            }

            if (!string.IsNullOrEmpty(skype))
            {
                Skype = skype;
            }

            if (!string.IsNullOrEmpty(cellPhone))
            {
                CellPhone = cellPhone;
            }
                        
            if (cannotLoginAfter.HasValue)
            {
                CannotLoginAfter = cannotLoginAfter.Value;
            }

            if (cannotLoginBefore.HasValue)
            {
                CannotLoginBefore = cannotLoginBefore.Value;
            }

            if (cannotLoginAfter != null && cannotLoginBefore != null && cannotLoginBefore.Value <= cannotLoginAfter.Value)
            {
                Status = cannotLoginAfter.Value.Date < DateTime.Now.Date ? StaffStatus.InActive : StaffStatus.Active;
            }
            else
            {
                Status = StaffStatus.InActive;
            }

            if (startDate.HasValue)
            {
                StartDate = startDate.Value;
            }
        }

        public void AssignLocation(Location location)
        {
            Location = location;
        }
        
        public void AssignToSubContractor(SubContractor subContractor)
        {
            SubContractors ??= new List<SubContractor>();
            if (!SubContractors.Contains(subContractor))
            {
                SubContractors.Add(subContractor);
            }
        }

        public bool AssignToProject(Project.Project project)
        {
            Projects ??= new List<Project.Project>();
            if (!Projects.Contains(project))
            {
                Projects.Add(project);
                return true;
            }

            return false;
        }

        public bool RemoveProject(Project.Project project)
        {
            if (Projects != null && (Projects.Any() || Projects.Contains(project)))
            {
                return Projects.Remove(project);
            }

            return false;
        }

    }


    public enum StaffStatus
    {
        [Description("Active")] Active = 1,
        [Description("In Active")] InActive
    }
}