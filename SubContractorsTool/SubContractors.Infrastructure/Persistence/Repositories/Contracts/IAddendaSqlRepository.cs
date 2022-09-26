using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SubContractors.Common.EfCore.Contracts;
using System.Threading.Tasks;
using SubContractors.Domain.Agreement;

namespace SubContractors.Infrastructure.Persistence.Repositories.Contracts
{
    public interface IAddendaSqlRepository : ICustomSqlRepository, ISqlRepository<Addendum, int>
    {
        Task<Addendum> GetAsync(int id);
        public  Task<IEnumerable<Addendum>> FindAsync(Expression<Func<Addendum, bool>> predicate);
    }
}