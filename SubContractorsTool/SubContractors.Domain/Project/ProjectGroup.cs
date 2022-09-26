using System;
using SubContractors.Common.EfCore;
using SubContractors.Domain.SubContractor.Staff;
using System.Collections.Generic;

namespace SubContractors.Domain.Project
{
    public class ProjectGroup : Entity<int>
    {
        public ProjectGroup()
        {
            
        }

        public ProjectGroup(int id)
        {
            Id = id;
        }

        public ProjectGroup(int id, int pmId, string name)
        {
            Id = id;
            PmId = pmId;
            Name = name;
        }
        public int PmId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public Staff DeliveryManager { get; set; }
        public List<Project> Projects { get; set; }

        public void Create(int pmId, string name)
        {
            Name = name;
            PmId = pmId;
        }
    }
}