using Domain.Models;

namespace LibApp.Services.Interfaces
{
    public interface IAuthorService
    {
        Task AddAuthorAsync(Author author);
    }
}
