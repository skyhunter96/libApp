using LibApp.Domain.Models.Common;

namespace LibApp.Domain.Models;

public class Author : BaseEntity
{
    public string Name { get; set; } = null!;
    public ICollection<Book> Books { get; protected set; } = new List<Book>();

    public void AddBooks(IEnumerable<Book> books)
    {
        if (books == null) return;
        foreach (var book in books)
        {
            if (!Books.Contains(book))
                Books.Add(book);
        }
    }
}
