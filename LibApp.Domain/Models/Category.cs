using LibApp.Domain.Models.Common;

namespace LibApp.Domain.Models;

public class Category : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public virtual ICollection<Book> Books { get; protected set; } = new List<Book>();
}
