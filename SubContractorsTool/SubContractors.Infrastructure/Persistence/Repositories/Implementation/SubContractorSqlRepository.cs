using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SubContractors.Common.EfCore;
using SubContractors.Domain.SubContractor;
using SubContractors.Infrastructure.Persistence.EfCore;
using SubContractors.Infrastructure.Persistence.Repositories.Contracts;

namespace SubContractors.Infrastructure.Persistence.Repositories.Implementation
{
    public class SubContractorSqlRepository : SqlRepository<SubContractor, SubContractorsDbContext, int>, ISubContractorSqlRepository
    {
        public SubContractorSqlRepository(SubContractorsDbContext context) : base(context)
        { }

        public async Task<SubContractor> GetWithProjectRelatedEntitiesAsync(int id)
        {
            return await Set.Include(x => x.Agreements)
                .ThenInclude(x => x.Addenda)
                .ThenInclude(x => x.Projects)
                .Include(x => x.Invoices)
                .ThenInclude(x => x.Project)
                .Include(x=>x.Projects)
                .FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<SubContractor> GetWithStaffRelatedEntitiesAsync(int id)
        {
            return await Set.Include(x => x.Agreements)
                .ThenInclude(x => x.Addenda)
                .ThenInclude(x => x.Staffs)
                .Include(x=>x.Staffs)
                .FirstOrDefaultAsync(x => x.Id == id);

        }
    }
}
