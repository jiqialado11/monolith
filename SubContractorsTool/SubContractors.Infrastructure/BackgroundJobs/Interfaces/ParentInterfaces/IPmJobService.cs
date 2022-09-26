using System.Threading.Tasks;

namespace SubContractors.Infrastructure.BackgroundJobs.Interfaces.ParentInterfaces
{
    public interface IPmJobService
    {
        Task MigratePmDataAsync();
        Task SynchronizePmDataAsync();
    }
}
