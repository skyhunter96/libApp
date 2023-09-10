using Domain.Models;

namespace LibApp.Services.Interfaces;

public interface IBookService
{
    IEnumerable<Book> GetBooks();
    Task<IEnumerable<Book>> GetBooksAsync();
    Task<Book> GetBookAsync(int id);

}