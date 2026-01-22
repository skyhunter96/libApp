using LibApp.Data.Abstractions.Interfaces.Common;
using LibApp.Domain.Models;

namespace LibApp.Data.Abstractions.Interfaces;

public interface IPublisherRepository : IGenericRepository<Publisher>
{
    Task<IEnumerable<Publisher>> GetAllWithUsersAsync();
    Task<Publisher?> GetByIdWithUsersAsync(int id);
}