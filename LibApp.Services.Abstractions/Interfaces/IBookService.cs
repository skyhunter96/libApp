using LibApp.Domain.Models;

namespace LibApp.Services.Abstractions.Interfaces;

public interface IBookService
{
    Task<IEnumerable<Book>> GetBooksAsync();
    Task<Book?> GetBookAsync(int id);
    Task<IEnumerable<int>> GetBookIdsByAuthorIdAsync(int authorId);
    Task AddBookAsync(Book book, IEnumerable<int>? existingAuthorIds, string? newAuthorName);
    Task UpdateBookAsync(Book book, IEnumerable<int>? selectedAuthorIds, string? newAuthorName);
    Task RemoveBookAsync(Book book);
    Task<bool> IsbnExistsAsync(string isbn);
    Task<bool> IsbnExistsInOtherBooksAsync(int id, string isbn);
}