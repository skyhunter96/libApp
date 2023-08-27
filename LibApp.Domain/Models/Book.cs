using Domain.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Book : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Isbn { get; set; }
    public string Edition { get; set; }
    public int PublisherId { get; set; }
    public int CategoryId { get; set; }
    public int? DepartmentId { get; set; }
    public int LanguageId { get; set; }
    public string ImagePath { get; set; }
    public decimal? Cost { get; set; }
    [Display(Name = "Available")]
    public bool IsAvailable { get; set; }
    public int Quantity { get; set; }
    [Display(Name = "Available Quantity")]
    public int AvailableQuantity { get; set; }
    [Display(Name = "Reserved Quantity")]
    public int ReservedQuantity { get; set; }

    public virtual Publisher Publisher { get; set; }
    public virtual Category Category { get; set; }
    public virtual Department Department { get; set; }
    public virtual Language Language { get; set; }
    public virtual ICollection<Author> Authors { get; set; }
    public ICollection<BookReservation> BookReservations { get; set; }
}
