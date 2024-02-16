using Domain.Models;

namespace LibApp.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAuthorsAsync();
        Task<Author> GetAuthorAsync(int id);
        Task AddAuthorAsync(Author author);
        Task UpdateAuthorAsync(Author author);
        Task RemoveAuthorAsync(Author author);
    }
}
