namespace LibApp.Domain.Models;

public class Language
{
    public int Id { get; init; }
    public string Name { get; set; } = null!;
    public virtual ICollection<Book> Books { get; protected set; } = new List<Book>();
}
