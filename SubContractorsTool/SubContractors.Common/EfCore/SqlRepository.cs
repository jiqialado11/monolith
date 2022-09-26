using Microsoft.EntityFrameworkCore;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.EfCore.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace SubContractors.Common.EfCore
{
    public class SqlRepository<TEntity, TDContext, TId> : ISqlRepository<TEntity, TId> where TEntity : class, IEntity<TId> where TDContext : DbContext
    {
        protected DbSet<TEntity> Set { get; set; }

        public SqlRepository(TDContext context)
        {
            Set = context.Set<TEntity>();
        }

        public virtual async Task<TEntity> GetAsync(TId id, string[] includes = null)
        {
            if (includes != null && includes.Length > 0)
            {
                var table = Set.Where(x => (object) x.Id == (object) id)
                               .AsQueryable();

                foreach (var include in includes)
                {
                    table = table.Include(include);
                }

                return await table.FirstOrDefaultAsync();
            }

            return await GetAsync(e => (object) e.Id == (object) id);
        }

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, string[] includes = null)
        {
            if (includes != null && includes.Length > 0)
            {
                var table = Set.Where(predicate)
                               .AsQueryable();
                foreach (var include in includes)
                {
                    table = table.Include(include);
                }

                return await table.FirstOrDefaultAsync();
            }

            return await Set.FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, string[] includes = null)
        {
            if (includes != null && includes.Length > 0)
            {
                var table = Set.Where(predicate)
                               .AsQueryable();
                foreach (var include in includes)
                {
                    table = table.Include(include);
                }

                return await table.ToListAsync();
            }

            return await Set.Where(predicate)
                            .ToListAsync();
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await Set.AddAsync(entity);
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await Set.AddRangeAsync(entities);
        }
        public virtual async Task UpdateAsync(TEntity entity)
        {
            await Task.Run(() => Set.Update(entity));
        }

        public virtual async Task DeleteAsync(TId id)
        {
            await Task.Run(async () => Set.Remove(await GetAsync(id)));
        }

        public virtual async Task<PagedResult<TEntity>> BrowseAsync<TQuery>(
            Expression<Func<TEntity, bool>> predicate, 
            TQuery query, 
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null) 
            where TQuery : PagedQueryBase
        {
            var table = Set.Where(predicate);

            if (include != null)
            {
                table = include(table);
            }

            return await table.PaginateAsync(query);
        }

        public virtual async Task<PagedResult<TEntity>> BrowseAsync<TQuery>(
            Expression<Func<TEntity, bool>> predicate,
            TQuery query,
            string[] includes = null)
            where TQuery : PagedQueryBase
        {
            var table = Set.Where(predicate);

            if (includes != null && includes.Any())
            {
                foreach (var include in includes)
                {
                    table = table.Include(include);
                }
            }

            return await table.PaginateAsync(query);
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Set.FirstOrDefaultAsync(predicate) != null;
        }
    }
}