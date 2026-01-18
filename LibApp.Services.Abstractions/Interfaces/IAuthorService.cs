using LibApp.Domain.Models;

namespace LibApp.Services.Abstractions.Interfaces;

public interface IAuthorService
{
    Task<IEnumerable<Author>> GetAuthorsAsync();
    Task<Author?> GetAuthorAsync(int id);
    Task AddAuthorAsync(Author author);
    Task UpdateAuthorAsync(Author author);
    Task RemoveAuthorAsync(Author author);
    bool AuthorExists(string name);
    bool AuthorExistsInOtherAuthors(int id, string name);
    bool IsDeletable(Author author);
}