using SubContractors.Common.EfCore;

namespace SubContractors.Domain.SubContractor.Tax
{
    public class TaxType : Entity<int>
    {
        public TaxType()
        {
            
        }

        public TaxType(int id)
        {
            Id = id;
        }

        public TaxType(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}