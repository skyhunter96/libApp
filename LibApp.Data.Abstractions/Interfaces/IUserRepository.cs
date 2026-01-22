using LibApp.Data.Abstractions.Interfaces.Common;
using LibApp.Domain.Models;

namespace LibApp.Data.Abstractions.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<IEnumerable<User>> GetAllWithRolesAsync();
    Task<User?> GetByIdWithRolesAsync(int id);
    Task SeverRelationsAsync(User user);
    Task<IEnumerable<Role>> GetAllRolesAsync();
    Task<Role?> GetRoleForUserIdAsync(int id);
}