namespace Domain.Models
{
    public class BookReservation
    {
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public bool Status { get; set; }

        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int CreatedByUserId { get; set; }
        public int ModifiedByUserId { get; set; }
    }
}
