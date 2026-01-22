using LibApp.Data.Abstractions.Interfaces.Common;
using LibApp.Domain.Models;

namespace LibApp.Data.Abstractions.Interfaces;

public interface IBookRepository : IGenericRepository<Book>
{
    Task<IEnumerable<Book>> GetAllWithRelatedEntitiesAsync();
    Task<Book?> GetByIdWithUsersAsync(int id);
    Task<IEnumerable<int>> GetBookIdsByAuthorIdAsync(int authorId);
    Task AddAsync(Book book, IEnumerable<int>? existingAuthorIds, string? newAuthorName);
    Task UpdateAsync(Book book, IEnumerable<int>? selectedAuthorIds, string? newAuthorName);
}