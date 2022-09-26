using System.ComponentModel;

namespace SubContractors.Domain.Check
{
    public enum CheckStatus
    {
        [Description("Passed")]
        Passed = 1,
        [Description("Not passed")]
        NotPassed = 2
    }
}
