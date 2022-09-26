using System.Threading.Tasks;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Domain.SubContractor;

namespace SubContractors.Infrastructure.Persistence.Repositories.Contracts
{
    public interface ISubContractorSqlRepository : ICustomSqlRepository, ISqlRepository<SubContractor, int>
    {
        Task<SubContractor> GetWithProjectRelatedEntitiesAsync(int id);

        Task<SubContractor> GetWithStaffRelatedEntitiesAsync(int id);
    }
}
