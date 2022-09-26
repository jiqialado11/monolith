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
    public class AgreementRepository : SqlRepository<Agreement, SubContractorsDbContext, int>, IAgreementSqlRepository
    {
        public AgreementRepository(SubContractorsDbContext context) : base(context)
        { }

        public async Task<Agreement> GetAsync(int id)
        {
            return await Set.Where(x => x.Id == id)
                .Include(x => x.LegalEntity)
                .Include(x => x.SubContractor)
                .Include(x => x.BudgetOffice)
                .Include(x => x.PaymentMethod)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Agreement>> FindAsync(Expression<Func<Agreement, bool>> predicate)
        {
            return await Set.Where(predicate)
                            .Include(a => a.Addenda)
                            .ThenInclude(ad => ad.PaymentTerm)
                            .Include(a => a.LegalEntity)
                            .Include(a => a.PaymentMethod)
                            .Include(a => a.BudgetOffice)
                            .ToListAsync();
        }
    }
}