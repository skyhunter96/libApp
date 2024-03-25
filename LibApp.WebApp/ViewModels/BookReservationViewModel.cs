using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace LibApp.WebApp.ViewModels
{
    public class BookReservationViewModel
    {
        public int ReservationId { get; set; }
        public int BookId { get; set; }

        [Display(Name = "Created")]
        public DateTime CreatedDateTime { get; set; }

        [Display(Name = "Modified")]
        public DateTime ModifiedDateTime { get; set; }
    }
}
