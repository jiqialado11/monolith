using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreenPipes.Caching;
using Microsoft.EntityFrameworkCore;
using SubContractors.Common.EfCore;
using SubContractors.Domain.Project;
using SubContractors.Infrastructure.Persistence.EfCore;
using SubContractors.Infrastructure.Persistence.Repositories.Contracts;

namespace SubContractors.Infrastructure.Persistence.Repositories.Implementation
{
    public class ProjectSqlRepository : SqlRepository<Project, SubContractorsDbContext, Guid>, IProjectSqlRepository
    {
        private SubContractorsDbContext _context;
        public ProjectSqlRepository(SubContractorsDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Project>> GetProjectsBySubContractorsIdentifiers(IList<int> ids)
        {
            return await Set.Include(x => x.ProjectGroup)
                .Include(x => x.Staffs)
                .Include(x => x.SubContractors)
                .Include(x => x.ProjectManager)
                .Where(x => x.SubContractors.Any(sub=> ids.Contains(sub.Id))).ToListAsync();
        }
    }
}
