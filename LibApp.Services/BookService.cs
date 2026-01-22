using LibApp.Data.Abstractions.Interfaces;
using LibApp.Domain.Models;
using LibApp.Services.Abstractions.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace LibApp.Services;

public class BookService(IBookRepository bookRepository) : IBookService
{
    public async Task<IEnumerable<Book>> GetBooksAsync()
    {
        return await bookRepository.GetAllWithRelatedEntitiesAsync();
    }

    public async Task<Book?> GetBookAsync(int id)
    {
        return await bookRepository.GetByIdWithUsersAsync(id);
    }

    public async Task<IEnumerable<int>> GetBookIdsByAuthorIdAsync(int authorId)
    {
        return await bookRepository.GetBookIdsByAuthorIdAsync(authorId);
    }

    public async Task AddBookAsync(Book book, IEnumerable<int>? existingAuthorIds, string? newAuthorName)
    {
        await bookRepository.AddAsync(book, existingAuthorIds, newAuthorName);
    }

    public async Task UpdateBookAsync(Book book, IEnumerable<int>? selectedAuthorIds, string? newAuthorName)
    {
        await bookRepository.UpdateAsync(book, selectedAuthorIds, newAuthorName);
    }

    public async Task RemoveBookAsync(Book book)
    {
        await bookRepository.RemoveAsync(book);
    }

    public async Task<bool> IsbnExistsAsync(string isbn)
    {
        if (isbn.IsNullOrEmpty())
            return false;

        var books = await bookRepository.GetAllAsync();
        var book = books.FirstOrDefault(bookCompared => bookCompared.Isbn == isbn);
        return book != null;
    }

    public async Task<bool> IsbnExistsInOtherBooksAsync(int id, string isbn)
    {
        if (id == 0 || isbn.IsNullOrEmpty())
            return false;

        var books = await bookRepository.GetAllAsync();
        var exists = books.Any(book => book.Id != id && book.Isbn == isbn);
        return exists;
    }
}