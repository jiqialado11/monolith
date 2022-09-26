using System;
using System.Threading.Tasks;

namespace SubContractors.Infrastructure.BackgroundJobs.Interfaces.ParentInterfaces
{
    public interface IMdpJobService
    {
       Task MigrateMdpDataAsync();
       Task SynchronizeMdpDataAsync();
    }
}
