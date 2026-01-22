using LibApp.Data.Abstractions.Interfaces.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibApp.EfDataAccess.Repositories.Common;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly LibraryContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(LibraryContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<T>();
    }

    public virtual IQueryable<T> Query()
    {
        //TODO: consider tracking options
        return _dbSet.AsNoTracking();
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await Query().ToListAsync();
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        return entity;
    }

    public virtual async Task AddAsync(T entity)
    {
        _dbSet.Add(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task RemoveAsync(T entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.AnyAsync(predicate);
    }
}