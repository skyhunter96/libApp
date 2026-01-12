namespace LibApp.Domain.Models
{
    public class BookReservation
    {
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }

        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
    }
}
