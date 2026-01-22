using LibApp.Data.Abstractions.Interfaces.Common;
using LibApp.Domain.Models;

namespace LibApp.Data.Abstractions.Interfaces;

public interface IAuthorRepository : IGenericRepository<Author>
{
    Task<IEnumerable<Author>> GetAllWithUsersAsync();
    Task<Author?> GetByIdWithUsersAsync(int id);
}