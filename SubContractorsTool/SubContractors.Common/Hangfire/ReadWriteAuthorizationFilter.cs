using Hangfire.Dashboard;

namespace SubContractors.Common.Hangfire
{
    public class ReadWriteAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context) => true;
    }
}
