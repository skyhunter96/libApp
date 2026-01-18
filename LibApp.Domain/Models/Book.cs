using LibApp.Domain.Models.Common;

namespace LibApp.Domain.Models;

public class Book : BaseEntity
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Isbn { get; set; } = null!;
    public string Edition { get; set; } = null!;
    public int ReleaseYear { get; set; }
    public int PublisherId { get; set; }
    public int CategoryId { get; set; }
    public int? DepartmentId { get; set; }
    public int LanguageId { get; set; }
    public string ImagePath { get; set; } = null!;
    public decimal? Cost { get; set; }
    public bool IsAvailable { get; set; }
    public int Quantity { get; set; }
    public int AvailableQuantity { get; set; }
    public int ReservedQuantity { get; set; }

    public virtual Publisher Publisher { get; set; } = null!;
    public virtual Category Category { get; set; } = null!;
    public virtual Department? Department { get; set; }
    public virtual Language Language { get; set; } = null!;
    public virtual ICollection<Author> Authors { get; protected set; } = new List<Author>();
    public ICollection<BookReservation> BookReservations { get; protected set; } = new List<BookReservation>();

    public void AddAuthors(IEnumerable<Author> authors)
    {
        if (authors == null) return;
        foreach (var author in authors)
        {
            if (!Authors.Contains(author))
                Authors.Add(author);
        }
    }
}
