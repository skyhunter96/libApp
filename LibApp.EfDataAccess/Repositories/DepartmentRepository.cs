using LibApp.Data.Abstractions.Interfaces;
using LibApp.Domain.Models;
using LibApp.EfDataAccess.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace LibApp.EfDataAccess.Repositories;

public class DepartmentRepository(LibraryContext context) : GenericRepository<Department>(context), IDepartmentRepository
{
    public async Task<IEnumerable<Department>> GetAllWithUsersAsync()
    {
        return await Query()
            .Include(department => department.CreatedByUser)
            .Include(department => department.ModifiedByUser)
            .ToListAsync();
    }

    public async Task<Department?> GetByIdWithUsersAsync(int id)
    {
        return await Query()
            .Include(department => department.CreatedByUser)
            .Include(department => department.ModifiedByUser)
            .FirstOrDefaultAsync(department => department.Id == id);
    }
}