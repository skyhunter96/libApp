using System.ComponentModel.DataAnnotations;

namespace LibApp.WebApp.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public bool IsVerified { get; set; }
        public DateTime? LastLoginDateTime { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }
        public bool IsCardActive { get; set; }
        public string ImagePath { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string CardCode { get; set; }
        public decimal TotalFee { get; set; }
        public string Currency { get; set; }
        public string Notes { get; set; }
        public string? Role { get; set; }
        public string? CreatedByUser { get; set; }
        public string? ModifiedByUser { get; set; }
    }
}
