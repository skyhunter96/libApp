using Domain.Models.Common;

namespace Domain.Models;

public class Category : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual ICollection<Book> Books { get; set; }
}
