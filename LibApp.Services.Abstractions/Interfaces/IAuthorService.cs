using LibApp.Domain.Models;

namespace LibApp.Services.Abstractions.Interfaces;

public interface IAuthorService
{
    Task<IEnumerable<Author>> GetAuthorsAsync();
    Task<Author?> GetAuthorAsync(int id);
    Task AddAuthorAsync(Author author);
    Task UpdateAuthorAsync(Author author);
    Task RemoveAuthorAsync(Author author);
    Task<bool> AuthorExistsAsync(string name);
    Task<bool> AuthorExistsInOtherAuthorsAsync(int id, string name);
    Task<bool> IsDeletableAsync(int id);
}