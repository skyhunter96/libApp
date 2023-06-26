using Domain.Models.Common;

namespace Domain.Models;

public class User : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public DateTime RegistrationDateTime { get; set; }
    public bool Verified { get; set; }
    public string VerificationToken { get; set; }
    public DateTime? VerificationSentAt { get; set; }
    public DateTime? LastLoginDateTime { get; set; }
    public bool Active { get; set; }
    public int RoleId { get; set; }
    public string ImagePath { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string CardCode { get; set; }
    public bool IsCardActive { get; set; }
    public decimal TotalFee { get; set; }
    public string Currency { get; set; }
    public string Notes { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; }
    public Role Role { get; set; }
}
