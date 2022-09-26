using SubContractors.Common.EfCore;

namespace SubContractors.Domain.Compliance
{
    public class ComplianceRating : Entity<int>
    {
        public string Value { get; set; }
        public string Description { get; set; }

        public ComplianceRating(int id, string value, string description)
        {
            Id = id;
            Value = value;
            Description = description;
        }
    }
}