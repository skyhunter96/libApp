using Domain.Models.Common;

namespace Domain.Models;

public class User : BaseEntity
{
    public int RoleId { get; set; }
    public virtual ICollection<Reservation> Reservations { get; set; }
    public virtual Role Role { get; set; }
}
