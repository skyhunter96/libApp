namespace LibApp.Domain.Models;

public class BookReservation
{
    public int ReservationId { get; set; }
    public Reservation Reservation { get; init; } = null!;
    public int BookId { get; set; }
    public Book Book { get; set; } = null!;

    public DateTime CreatedDateTime { get; init; }
    public DateTime ModifiedDateTime { get; protected set; }

    public void SetModifiedDateTime(DateTime dateTime) => ModifiedDateTime = dateTime;
}