using Domain.Models;

namespace LibApp.Services.Interfaces;

public interface IBookService
{
    Task<IEnumerable<Book>> GetBooksAsync();
    Task<Book> GetBookAsync(int id);

}