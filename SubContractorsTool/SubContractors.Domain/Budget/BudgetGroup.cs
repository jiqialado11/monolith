using SubContractors.Common.EfCore;
using System.Collections.Generic;

namespace SubContractors.Domain.Budget
{
    public class BudgetGroup : Entity<int>
    {
        public int? BudgetSystemId { get; set; }
        public string BudgetGroupName { get; set; }
        public bool IsDefault { get; set; }
        public string EmailAddress { get; set; }
        public bool IsActive { get; set; }
        public bool IsFunction { get; set; }
        public int? ParentBudgetGroupId { get; set; }
        public BudgetGroup ParentBudgetGroup { get; set; }
        public List<Project.Project> Projects { get; set; }

        public BudgetGroup()
        { }
        public BudgetGroup(int id)
        {
            Id = id;
        }

        public BudgetGroup(int id, string budgetGroupName, bool isDefault, string emailAddress, bool isActive, bool isFunction, int? parentBudgetGroupId)
        {
            Id = id;
            BudgetGroupName = budgetGroupName;
            IsDefault = isDefault;
            EmailAddress = emailAddress;
            IsActive = isActive;
            IsFunction = isFunction;

            ParentBudgetGroupId = parentBudgetGroupId;
        }
    }
}