using LibApp.Domain.Models.Common;

namespace LibApp.Domain.Models;

public class Reservation : BaseEntity
{
    public DateTime? LoanDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ActualReturnDate { get; set; }
    public decimal LateFee { get; set; }
    public bool IsStarted { get; set; }
    public int? ReservedByUserId { get; init; }

    public virtual User? ReservedByUser { get; init; }
    public virtual ICollection<BookReservation> BookReservations { get; protected set; } = new List<BookReservation>();

    public void AddBookReservations(BookReservation bookReservation)
    {
        ArgumentNullException.ThrowIfNull(bookReservation);

        if (!BookReservations.Contains(bookReservation))
                BookReservations.Add(bookReservation);
    }
}
