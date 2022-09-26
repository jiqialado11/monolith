using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Domain.Project;

namespace SubContractors.Infrastructure.Persistence.Repositories.Contracts
{
    public interface IProjectSqlRepository : ICustomSqlRepository, ISqlRepository<Project, Guid>
    {
        Task<IEnumerable<Project>> GetProjectsBySubContractorsIdentifiers(IList<int> ids);
    }
}
