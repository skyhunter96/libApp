using Domain.Models;

namespace LibApp.Services.Interfaces;

public interface IBookService
{
    IEnumerable<Book> GetBooks();
    Task<IEnumerable<Book>> GetBooksAsync();
    Task<Book> GetBookAsync(int id);

    bool IsbnExists(string isbn);
    Task AddBookAsync(Book book, IEnumerable<int>? existingAuthorIds, string? newAuthorName);
}