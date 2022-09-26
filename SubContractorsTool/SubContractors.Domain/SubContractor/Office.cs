using SubContractors.Common.EfCore;
using System.Collections.Generic;

namespace SubContractors.Domain.SubContractor
{
    public class Office : Entity<int>
    {
        public string Name { get; set; }
        public OfficeType OfficeType { get; set; }
        public List<SubContractor> SubContractors { get; set; }

        public Office(int id, string name, OfficeType officeType)
        {
            Id = id;
            Name = name;
            OfficeType = (OfficeType)officeType;
        }
    }

    public enum OfficeType
    {
        SalesOffice = 1,
        DevelopmentOffice
    }
}