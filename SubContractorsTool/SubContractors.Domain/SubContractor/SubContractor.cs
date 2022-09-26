using SubContractors.Common.EfCore;
using SubContractors.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using SubContractors.Common.EfCore.Contracts;

namespace SubContractors.Domain.SubContractor
{
    public class SubContractor : Entity<int>, IArchivable
    {
        public SubContractor()
        { }

        public SubContractor(int id)
        {
            Id = id;
        }

        public int? MdpId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Office> Offices { get; set; }
        public string WebSite { get; set; }
        public string Comment { get; set; }
        public DateTime LastInteractionDate { get; set; }
        public bool IsNDASigned { get; set; }
        public Location Location { get; set; }
        public int? AccountManagerId { get; set; }
        public Staff.Staff AccountManager { get; set; }
        public List<Staff.Staff> Staffs { get; set; }
        public SubContractorType SubContractorType { get; set; }
        public SubContractorStatus SubContractorStatus { get; set; }
        public string Skills { get; set; }
        public string Contact { get; set; }
        public List<Agreement.Agreement> Agreements { get; set; }
        public List<Project.Project> Projects { get; set; }
        public List<Tax.Tax> Taxes { get; set; }
        public List<Compliance.Compliance> Compliances { get; set; }
        public List<Check.SanctionCheck> SanctionChecks { get; set; }
        public List<Check.BackgroundCheck> BackgroundChecks { get; set; }
        public List<Invoice.Invoice> Invoices { get; set; }
        public List<Market> Markets { get; set; }
        public string Materials { get; set; }
        public bool IsArchived { get; set; }
        public string ExternalId { get; set; }

        public void Create(string name, SubContractorType type, SubContractorStatus status, Location location, string comment, DateTime lastInteractionDate,
            bool isNdaSigned, string skills, string contact, string companySite, string materials)
        {
            Name = name;
            SubContractorType = type;
            SubContractorStatus = status;
            Location = location;
            Comment = comment;
            LastInteractionDate = lastInteractionDate;
            IsNDASigned = isNdaSigned;
            Skills = skills;
            Contact = contact;
            WebSite = companySite;
            Materials = materials;
            IsDeleted = false;
            IsArchived = false;
        }

        public void Update(string name, SubContractorType type, SubContractorStatus status, Location location, string comment, DateTime lastInteractionDate,
            bool isNdaSigned, string skills, string contact, string companySite, string materials)
        {
            Name = name;
            SubContractorType = type;
            SubContractorStatus = status;
            Location = location;
            Comment = comment;
            LastInteractionDate = lastInteractionDate;
            IsNDASigned = isNdaSigned;
            Skills = skills;
            Contact = contact;
            WebSite = companySite;
            Materials = materials;
            IsDeleted = false;
            IsArchived = false;
        }

        public void AddOffice(Office office)
        {
            Offices ??= new List<Office>();
            if (!Offices.Contains(office))
            {
                Offices.Add(office);
            }
        }

        public bool RemoveOffice(Office office)
        {
            if (Offices.Any() || Offices.Contains(office))
            {
                return Offices.Remove(office);
            }

            return false;
        }
        
        public void AddMarket(Market market)
        {
            Markets ??= new List<Market>();

            if (!Markets.Contains(market))
            {
                Markets.Add(market);
            }
        }

        public bool RemoveMarket(Market market)
        {
            if (Markets.Any() || Markets.Contains(market))
            {
                return Markets.Remove(market);
            }

            return false;
        }
        public void AssignAgreement(Agreement.Agreement agreement)
        {
            Agreements ??= new List<Agreement.Agreement>();

            if (!Agreements.Contains(agreement))
            {
                Agreements.Add(agreement);
            }
        }
        public bool AssignProject(Project.Project project)
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


        public void AssignCompliance(Compliance.Compliance compliance)
        {
            Compliances ??= new List<Compliance.Compliance>();

            if (!Compliances.Contains(compliance))
            {
                Compliances.Add(compliance);
            }
        }

        public bool AssignStaff(Staff.Staff staff)
        {
            Staffs ??= new List<Staff.Staff>();

            if (!Staffs.Select(x=>x.Id).ToList().Contains(staff.Id))
            {
                Staffs.Add(staff);
                return true;
            }

            return false;
        }

        public bool RemoveStaff(Staff.Staff staff)
        {

            if (Staffs != null && (Staffs.Any() || Staffs.Contains(staff)))
            {
                return Staffs.Remove(staff);
            }

            return false;
        }


        public void AssignAccountManager(Staff.Staff manager)
        {
            AccountManager = manager;
        }

        public void DeAssignAccountManager()
        {
            AccountManagerId = null;
        }

    }

    public enum SubContractorStatus
    {
        [Description("Active")]
        Active = 1,

        [Description("Inactive")]
        InActive = 2,
        
        [Description("Tentative")]
        Tentative = 3
    }

    public enum SubContractorType
    {
        [Description("Contractor")]
        Contractor = 1,

        [Description("Subcontractor")]
        SubContractor,

        [Description("Adviser")]
        Adviser,

        [Description("Reseller")]
        Reseller,

        [Description("Independent Consultant")]
        IndependentConsultant,

        [Description("Hr Partner")]
        HrPartner,

        [Description("Internal Contractor")]
        InternalContractor
    }
}