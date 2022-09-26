using SubContractors.Common.EfCore;

namespace SubContractors.Domain.Budget
{
    public class PaymentTerm : Entity<int>
    {
        public PaymentTerm()
        {
            
        }

        public PaymentTerm(int id)
        {
            Id = id;
        }

        public string Value { get; set; }
        public bool IsActive { get; set; }

        public PaymentTerm(int id, string value, bool isActive)
        {
            Id = id;
            Value = value;
            IsActive = isActive;
        }
    }
}