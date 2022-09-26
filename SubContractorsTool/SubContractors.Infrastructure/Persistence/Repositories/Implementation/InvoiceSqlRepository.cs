using Microsoft.EntityFrameworkCore;
using SubContractors.Common.EfCore;
using SubContractors.Common.EfCore.Pagination;
using SubContractors.Domain.Invoice;
using SubContractors.Infrastructure.Persistence.EfCore;
using SubContractors.Infrastructure.Persistence.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SubContractors.Infrastructure.Persistence.Repositories.Implementation
{
    public class InvoiceSqlRepository : SqlRepository<Invoice, SubContractorsDbContext, int>, IInvoiceSqlRepository
    {
        public InvoiceSqlRepository(SubContractorsDbContext context) : base(context)
        {
        }

        public async Task<PagedResult<Invoice>> BrowseAsync<TQuery>(Expression<Func<Invoice, bool>> predicate, TQuery query) where TQuery : PagedQueryBase
        {
            return await Set.Where(predicate)
                .Include(x => x.Referral)
                .Include(x => x.SubContractor)
                .ThenInclude(x => x.AccountManager)
                .Include(x => x.SubContractor)
                .Include(x => x.Addendum)
                .ThenInclude(x => x.Agreement)
                .ThenInclude(x => x.LegalEntity)
                .Include(x => x.Addendum)
                .ThenInclude(x => x.Currency)
                .Include(x => x.Addendum)
                .ThenInclude(x => x.PaymentTerm)
                .Include(x => x.Project)
                .ThenInclude(x => x.InvoiceApprover)
                .Include(x => x.BudgedInfo)
                .PaginateAsync(query);
        }

        public async Task<IEnumerable<Invoice>> FindAsync(Expression<Func<Invoice, bool>> predicate)
        {
            return await Set.Where(predicate)
                .Include(x => x.SubContractor)
                .ThenInclude(x => x.AccountManager)
                .Include(x => x.SubContractor)
                .Include(x => x.Addendum)
                .ThenInclude(x => x.Agreement)
                .ThenInclude(x => x.LegalEntity)
                .Include(x => x.Addendum)
                .ThenInclude(x => x.Currency)
                .Include(x => x.Addendum)
                .ThenInclude(x => x.PaymentTerm)
                .Include(x => x.Project)
                .ThenInclude(x => x.InvoiceApprover)
                .Include(x => x.BudgedInfo)
                .ToListAsync();
        }

        public async Task<Invoice> GetAsync(Expression<Func<Invoice, bool>> predicate)
        {
            return await Set.Where(predicate)
                .Include(x => x.MileStone)
                .Include(x => x.SubContractor)
                .Include(x => x.Addendum)
                .Include(x => x.Addendum)
                .ThenInclude(x => x.Currency)
                .Include(x => x.Addendum)
                .ThenInclude(x => x.Agreement)
                .Include(x => x.Project)
                .Include(x => x.Referral)
                .Include(x => x.BudgedInfo)
                .Include(x => x.SupportingDocuments)
                .Include(x => x.InvoiceFile)
                .FirstOrDefaultAsync();
        }
    }
}
