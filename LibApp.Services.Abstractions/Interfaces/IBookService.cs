using LibApp.Domain.Models;

namespace LibApp.Services.Abstractions.Interfaces;

//TODO: Interfaces to another project
public interface IBookService
{
    IEnumerable<Book> GetBooks();
    Task<IEnumerable<Book>> GetBooksAsync();
    Task<Book?> GetBookAsync(int id);
    IEnumerable<int> GetBookIdsByAuthorId(int authorId);
    Task AddBookAsync(Book book, IEnumerable<int>? existingAuthorIds, string? newAuthorName);
    Task UpdateBookAsync(Book book, IEnumerable<int>? selectedAuthorIds, string? newAuthorName);
    Task RemoveBookAsync(Book book);
    bool IsbnExists(string isbn);
    bool IsbnExistsInOtherBooks(int id, string isbn);
}