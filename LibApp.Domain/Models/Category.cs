using LibApp.Domain.Models.Common;

namespace LibApp.Domain.Models;

public class Category : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual ICollection<Book> Books { get; set; }
}
