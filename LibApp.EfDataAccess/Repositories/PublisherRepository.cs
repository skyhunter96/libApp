using LibApp.Data.Abstractions.Interfaces;
using LibApp.Domain.Models;
using LibApp.EfDataAccess.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace LibApp.EfDataAccess.Repositories;

public class PublisherRepository(LibraryContext context) : GenericRepository<Publisher>(context), IPublisherRepository
{
    public async Task<IEnumerable<Publisher>> GetAllWithUsersAsync()
    {
        return await Query()
            .Include(category => category.CreatedByUser)
            .Include(category => category.ModifiedByUser)
            .ToListAsync();
    }

    public async Task<Publisher?> GetByIdWithUsersAsync(int id)
    {
        return await Query()
            .Include(category => category.CreatedByUser)
            .Include(category => category.ModifiedByUser)
            .FirstOrDefaultAsync(publisher => publisher.Id == id);
    }
}