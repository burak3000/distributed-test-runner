using System;
using DistributedTestRunner.Core.Repositories;

namespace DistributedTestRunner.Core
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> CommitAsync();
        void Commit();

        IRepository<T> Repository<T>() where T : class;

    }
}

