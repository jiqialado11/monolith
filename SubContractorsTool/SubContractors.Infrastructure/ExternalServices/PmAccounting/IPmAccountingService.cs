using SubContractors.Infrastructure.ExternalServices.PmAccounting.ResponseModels;
using System.Threading.Tasks;

namespace SubContractors.Infrastructure.ExternalServices.PmAccounting
{
    public interface IPmAccountingService
    {
        Task<PmAccountingResponse> GetMilestonesAsync(int projectId);
    }
}
