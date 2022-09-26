using System.Collections.Generic;
using SubContractors.Common.EfCore;

namespace SubContractors.Domain.SubContractor
{
    public class Market : Entity<int>
    {
        public string Name { get; set; }

        public List<SubContractor> SubContractors { get; set; }

        public Market(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}