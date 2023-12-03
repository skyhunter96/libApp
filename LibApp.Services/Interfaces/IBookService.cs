using Domain.Models;

namespace LibApp.Services.Interfaces;

public interface IBookService
{
    IEnumerable<Book> GetBooks();
    Task<IEnumerable<Book>> GetBooksAsync();
    Task<Book> GetBookAsync(int id);

    Task AddBookAsync(Book book, IEnumerable<int>? existingAuthorIds, string? newAuthorName);
    Task UpdateBookAsync(Book book, IEnumerable<int>? selectedAuthorIds, string? newAuthor);
    Task RemoveBookAsync(Book book);
    bool IsbnExists(string isbn);
    bool IsbnExistsInOtherBooks(int id, string isbn);
}