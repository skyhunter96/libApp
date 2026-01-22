using LibApp.Data.Abstractions.Interfaces.Common;
using LibApp.Domain.Models;

namespace LibApp.Data.Abstractions.Interfaces;

public interface IDepartmentRepository : IGenericRepository<Department>
{
    Task<IEnumerable<Department>> GetAllWithUsersAsync();
    Task<Department?> GetByIdWithUsersAsync(int id);
}