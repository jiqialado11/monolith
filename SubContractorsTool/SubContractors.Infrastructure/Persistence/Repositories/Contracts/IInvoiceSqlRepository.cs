using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.EfCore.Pagination;
using SubContractors.Domain.Invoice;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SubContractors.Infrastructure.Persistence.Repositories.Contracts
{
    public interface IInvoiceSqlRepository : ICustomSqlRepository, ISqlRepository<Invoice, int>
    {
        Task<PagedResult<Invoice>> BrowseAsync<TQuery>(Expression<Func<Invoice, bool>> predicate, TQuery query) where TQuery : PagedQueryBase;
        Task<IEnumerable<Invoice>> FindAsync(Expression<Func<Invoice, bool>> predicate);
        Task<Invoice> GetAsync(Expression<Func<Invoice, bool>> predicate);

    }
}
