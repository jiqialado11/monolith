using Microsoft.EntityFrameworkCore;

namespace SubContractors.Infrastructure.Persistence.EfCore
{
    public class SubContractorsHangfireDbContext : DbContext
    {
        public SubContractorsHangfireDbContext(DbContextOptions<SubContractorsHangfireDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public SubContractorsHangfireDbContext()
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { }
    }
}
