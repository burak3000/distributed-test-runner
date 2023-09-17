using System;
using System.Linq.Expressions;

namespace DistributedTestRunner.Core.Repositories
{
	public interface IRepository<TEntity> where TEntity:class
	{
		Task<TEntity?> GetAsync(Guid id);
		Task<IEnumerable<TEntity>> GetAllAsync();
		Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);

		Task AddAsync(TEntity entity);
		Task AddRangeAsync(IEnumerable<TEntity> entities);

		Task RemoveAsync(TEntity entity);
		Task RemoveRangeAsync(IEnumerable<TEntity> entities);
	}
}

