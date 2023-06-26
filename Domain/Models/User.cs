using Domain.Models.Common;

namespace Domain.Models;

public class User : BaseEntity
{
    public virtual ICollection<Reservation> Reservations { get; set; }
    public Role Role { get; set; }
}
