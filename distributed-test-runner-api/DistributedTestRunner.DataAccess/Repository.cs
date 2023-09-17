using System;
using System.Linq.Expressions;
using DistributedTestRunner.Core.Repositories;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DistributedTestRunner.DataAccess
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext _dbContext;
        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbContext.Set<TEntity>().AddRangeAsync(entities);
        }

        public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            var found = await _dbContext.Set<TEntity>().Where(predicate).ToListAsync();
            return found;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var all = await _dbContext.Set<TEntity>().ToListAsync();
            return all;
        }

        public async Task<TEntity?> GetAsync(Guid id)
        {
            TEntity? found = await _dbContext.Set<TEntity>().FindAsync(id);
            return found;

        }

        public async Task RemoveAsync(TEntity entity)
        {
            await Task.Run(() =>
            {
                _dbContext.Set<TEntity>().Remove(entity);
            });
        }

        public async Task RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            await Task.Run(() =>
            {
                _dbContext.Set<TEntity>().RemoveRange(entities);
            });
        }
    }
}

