using Microsoft.EntityFrameworkCore;
using SubContractors.Common.EfCore;
using SubContractors.Domain.Agreement;
using SubContractors.Infrastructure.Persistence.EfCore;
using SubContractors.Infrastructure.Persistence.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SubContractors.Infrastructure.Persistence.Repositories.Implementation
{
    public class AddendaRepository : SqlRepository<Addendum, SubContractorsDbContext, int>, IAddendaSqlRepository
    {
        public AddendaRepository(SubContractorsDbContext context) : base(context)
        { }

        public async Task<Addendum> GetAsync(int id)
        {
            return await Set.Include(x => x.Agreement)
                    .ThenInclude(a => a.SubContractor)
                    .Include(x=> x.Agreement)
                    .ThenInclude(a=> a.LegalEntity)
                    .Include(x => x.Projects)
                    .Include(x => x.PaymentTerm)
                    .Include(x => x.Currency)
                    .FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<IEnumerable<Addendum>> FindAsync(Expression<Func<Addendum, bool>> predicate)
        {
            return await  Set.Where(predicate).Include(x => x.Agreement)
                .ThenInclude(a => a.SubContractor)
                .Include(x => x.Agreement)
                .ThenInclude(a => a.LegalEntity)
                .Include(x=>x.Agreement)
                .ThenInclude(x=>x.PaymentMethod)
                .Include(x => x.Projects)
                .Include(x => x.PaymentTerm)
                .Include(x => x.Currency).
            ToListAsync();

        }
    }
}