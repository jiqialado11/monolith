using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace SubContractors.Common.EfCore.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext Context { get; }
        int Save();
        Task<int> SaveAsync();
    }
}