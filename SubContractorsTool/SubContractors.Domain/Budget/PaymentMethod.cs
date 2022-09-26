using SubContractors.Common.EfCore;

namespace SubContractors.Domain.Budget
{
    public class PaymentMethod : Entity<int>
    {
        public int BudgetSystemId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public PaymentMethod()
        {

        }
        public PaymentMethod(int id)
        {
            Id = id;
        }

        public PaymentMethod(int id, string name, bool isActive, int budgetSystemId)
        {
            Id = id;
            Name = name;
            IsActive = isActive;
            BudgetSystemId = budgetSystemId;
        }
    }
}