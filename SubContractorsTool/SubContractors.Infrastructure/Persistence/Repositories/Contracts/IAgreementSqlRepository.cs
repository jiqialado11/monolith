using SubContractors.Common.EfCore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SubContractors.Domain.Agreement;

namespace SubContractors.Infrastructure.Persistence.Repositories.Contracts
{
    public interface IAgreementSqlRepository : ICustomSqlRepository, ISqlRepository<Agreement, int>
    {
        Task<IEnumerable<Agreement>> FindAsync(Expression<Func<Agreement, bool>> predicate);
        Task<Agreement> GetAsync(int id);
    }
}