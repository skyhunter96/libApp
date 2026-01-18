using Microsoft.AspNetCore.Identity;

namespace LibApp.Domain.Models;

public class User : IdentityUser<int>
{
    public string DocumentId { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Password { get; set; } = null!;

    //UserName, Pass, Email & Phone in Base Class

    public bool IsActive { get; set; }
    public int RoleId { get; set; }
    public string ImagePath { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }
    public string City { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string? CardCode { get; set; }
    public decimal TotalFee { get; set; }
    public string Currency { get; set; } = null!;
    public string? Notes { get; set; }

    //From BaseEntity:
    public DateTime CreatedDateTime { get; set; }
    public DateTime ModifiedDateTime { get; protected set; }
    public int? CreatedByUserId { get; protected set; }
    public int? ModifiedByUserId { get; protected set; }
    public virtual User? CreatedByUser { get; init; }
    public virtual User? ModifiedByUser { get; protected set; }

    public virtual ICollection<Reservation> Reservations { get; protected set; } = new List<Reservation>();
    public virtual Role Role { get; set; } = null!;

    public void SetModifiedDateTime(DateTime dt) => ModifiedDateTime = dt;
    public void SetCreatedByUserId(int? userId)
    {
        ModifiedByUserId = userId;
    }
    public void SetModifiedByUserId(int? userId)
    {
        if (userId != null)
            ModifiedByUserId = userId;
    }
}
