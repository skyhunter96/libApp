using LibApp.Data.Abstractions.Interfaces;
using LibApp.Domain.Models;
using LibApp.EfDataAccess.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace LibApp.EfDataAccess.Repositories;

public class AuthorRepository(LibraryContext context) : GenericRepository<Author>(context), IAuthorRepository
{
    public async Task<IEnumerable<Author>> GetAllWithUsersAsync()
    {
        return await Query()
            .Include(author => author.CreatedByUser)
            .Include(author => author.ModifiedByUser)
            .ToListAsync();
    }

    public async Task<Author?> GetByIdWithUsersAsync(int id)
    {
        return await Query()
            .Include(author => author.CreatedByUser)
            .Include(author => author.ModifiedByUser)
            .FirstOrDefaultAsync(author => author.Id == id);
    }
}