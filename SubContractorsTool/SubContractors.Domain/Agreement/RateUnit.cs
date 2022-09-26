using SubContractors.Common.EfCore;

namespace SubContractors.Domain.Agreement
{
    public class RateUnit : Entity<int>
    {
        public RateUnit()
        {
            
        }
        public RateUnit(int id)
        {
            Id = id;
        }
        public string Value { get; set; }

        public RateUnit(int id, string value)
        {
            Id = id;
            Value = value;
        }
    }
}