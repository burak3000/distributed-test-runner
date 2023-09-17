using System;
using DistributedTestRunner.Core;
using DistributedTestRunner.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DistributedTestRunner.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IRepository<T> Repository<T>() where T : class
        {
            return new Repository<T>(_context);
        }
    }
}

