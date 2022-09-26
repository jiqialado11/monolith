using SubContractors.Common.EfCore.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace SubContractors.Common.EfCore.Contracts
{
    public interface ISqlRepository<TEntity, in TId> where TEntity : class, IEntity<TId>
    {
        Task<TEntity> GetAsync(TId id, string[] includes = null);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, string[] includes = null);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, string[] includes = null);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TId id);
        Task<PagedResult<TEntity>> BrowseAsync<TQuery>(Expression<Func<TEntity, bool>> predicate, TQuery query, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include) where TQuery : PagedQueryBase;
        Task<PagedResult<TEntity>> BrowseAsync<TQuery>(Expression<Func<TEntity, bool>> predicate, TQuery query, string[] includes = null) where TQuery : PagedQueryBase;
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
    }
}