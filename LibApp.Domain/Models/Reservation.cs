using Domain.Models.Common;

namespace Domain.Models;

public class Reservation : BaseEntity
{
    public DateTime? LoanDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ActualReturnDate { get; set; }
    public decimal LateFee { get; set; }
    public int? ReservedByUserId { get; set; }

    public virtual User? ReservedByUser { get; set; }
    public ICollection<BookReservation> BookReservations { get; set; }
}
