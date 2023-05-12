using Domain.Models.Common;

namespace Domain.Models;

public class Book : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Isbn { get; set; }
    public string Edition { get; set; }
    public string Photo { get; set; }
    public decimal Cost { get; set; }
    public bool IsAvailable { get; set; }
    public int Quantity { get; set; }
    public int AvailableQuantity { get; set; }
    public int ReservedQuantity { get; set; }
}
