
using System.Threading.Tasks;

namespace SubContractors.Infrastructure.BackgroundJobs.Interfaces.ParentInterfaces
{
    public interface IBudgetJobService
    {
        Task MigrateBudgetDataAsync();
        Task SynchronizeBudgetDataAsync();
    }
}
