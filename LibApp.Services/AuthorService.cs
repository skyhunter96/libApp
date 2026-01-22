using LibApp.Data.Abstractions.Interfaces;
using LibApp.Domain.Models;
using LibApp.Services.Abstractions.Interfaces;

namespace LibApp.Services;

public class AuthorService(IAuthorRepository authorRepository, IBookRepository bookRepository) : IAuthorService
{
    public async Task<IEnumerable<Author>> GetAuthorsAsync()
    {
        return await authorRepository.GetAllWithUsersAsync();
    }

    public async Task<Author?> GetAuthorAsync(int id)
    {
        return await authorRepository.GetByIdWithUsersAsync(id);
    }

    public async Task AddAuthorAsync(Author author)
    {
        await authorRepository.AddAsync(author);
    }

    public async Task UpdateAuthorAsync(Author author)
    {
        author.SetModifiedDateTime(DateTime.UtcNow);
        await authorRepository.UpdateAsync(author);
    }

    //TODO: Should be just RemoveAsync by id
    public async Task RemoveAuthorAsync(Author author)
    {
        await authorRepository.RemoveAsync(author);
    }

    public Task<bool> AuthorExistsAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Task.FromResult(false);

        return authorRepository.AnyAsync(author => author.Name.ToLower() == name.ToLower());
    }

    public async Task<bool> AuthorExistsInOtherAuthorsAsync(int id, string name)
    {
        if (id == 0 || string.IsNullOrWhiteSpace(name))
            return false;

        return await authorRepository.AnyAsync(author => author.Id != id && author.Name.ToLower() == name.ToLower());
    }

    public async Task<bool> IsDeletableAsync(int id)
    {
        if (id == 0) return false;

        var isDeletable = !await bookRepository.AnyAsync(book => book.Authors.Any(author => author.Id == id));
        return isDeletable;
    }
}