using SubContractors.Common.EfCore;

namespace SubContractors.Domain.Common
{
    public class Currency : Entity<int>
    {
        public Currency()
        {
            
        }

        public Currency(int id)
        {
            Id = id;
        }

        public int BudgetSystemId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public Currency(int id, string name, string code, int budgetSystemId)
        {
            Id = id;
            Name = name;
            Code = code;
            BudgetSystemId = budgetSystemId;
        }
    }
}