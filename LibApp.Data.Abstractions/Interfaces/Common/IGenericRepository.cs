using System.Linq.Expressions;

namespace LibApp.Data.Abstractions.Interfaces.Common;

public interface IGenericRepository<T> where T : class
{
    IQueryable<T> Query(); // for flexible includes/filters
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task RemoveAsync(T entity);
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
}