using Microsoft.AspNetCore.Identity;

namespace Domain.Models;

public class User : IdentityUser<int>
{
    //From BaseEntity:
    public DateTime CreatedDateTime { get; set; }
    public DateTime ModifiedDateTime { get; set; }
    public int CreatedByUserId { get; set; }
    public int ModifiedByUserId { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }

    //UserName, Pass, Email & Phone in Base Class

    public bool IsVerified { get; set; }
    public string VerificationToken { get; set; }
    public DateTime? VerificationSentAt { get; set; }
    public DateTime? LastLoginDateTime { get; set; }
    public bool Active { get; set; }
    public int RoleId { get; set; }
    public string ImagePath { get; set; }

    //TODO: Check during registering how is entered
    public DateTime DateOfBirth { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public string CardCode { get; set; }
    public bool IsCardActive { get; set; }
    public decimal TotalFee { get; set; }
    public string Currency { get; set; }
    public string Notes { get; set; }

    //From BaseEntity:
    public virtual User CreatedByUser { get; set; }
    public virtual User ModifiedByUser { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; }
    public virtual Role Role { get; set; }
}
