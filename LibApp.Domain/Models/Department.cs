using LibApp.Domain.Models.Common;

namespace LibApp.Domain.Models;

public class Department : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? Location { get; set; }
    public decimal? Budget { get; set; }

    public virtual ICollection<Book> Books { get; protected set; } = new List<Book>();
}
