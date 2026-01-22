using LibApp.Data.Abstractions.Interfaces;
using LibApp.Domain.Models;
using LibApp.EfDataAccess.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace LibApp.EfDataAccess.Repositories;

public class CategoryRepository(LibraryContext context) : GenericRepository<Category>(context), ICategoryRepository
{
    public async Task<IEnumerable<Category>> GetAllWithUsersAsync()
    {
        return await Query()
            .Include(category => category.CreatedByUser)
            .Include(category => category.ModifiedByUser)
            .ToListAsync();
    }

    public async Task<Category?> GetByIdWithUsersAsync(int id)
    {
        return await Query()
            .Include(category => category.CreatedByUser)
            .Include(category => category.ModifiedByUser)
            .FirstOrDefaultAsync(category => category.Id == id);
    }
}