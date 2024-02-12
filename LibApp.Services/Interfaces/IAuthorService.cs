using Domain.Models;

namespace LibApp.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<Author> GetAuthorAsync(int id);
        Task AddAuthorAsync(Author author);
    }
}
