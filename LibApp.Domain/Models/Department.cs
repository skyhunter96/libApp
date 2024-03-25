using Domain.Models.Common;

namespace Domain.Models;

public class Department : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string? Location { get; set; }
    public decimal? Budget { get; set; }

    public virtual ICollection<Book> Books { get; set; }
}
