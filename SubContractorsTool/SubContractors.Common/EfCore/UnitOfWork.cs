using Microsoft.EntityFrameworkCore;
using SubContractors.Common.EfCore.Contracts;
using System.Threading.Tasks;

namespace SubContractors.Common.EfCore
{
    public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext, new()
    {
        private readonly TContext _context;

        public UnitOfWork(TContext context)
        {
            _context = context;
        }

        public DbContext Context => _context;

        public void Dispose()
        {
            _context.Dispose();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}