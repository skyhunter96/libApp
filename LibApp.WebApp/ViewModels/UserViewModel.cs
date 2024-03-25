using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace LibApp.WebApp.ViewModels
{
    public class UserViewModel : IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(50)]
        public string Password { get; set; }

        [Display(Name = "Phone Number")]
        [Required]
        [MaxLength(30)]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(13)]
        public string DocumentId { get; set; }

        public bool IsVerified { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [Display(Name = "CardActive")]
        public string? ImagePath { get; set; }

        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Date Of Birth")]
        public string? DateOfBirthToShow { get; set; }

        [Required]
        [MaxLength(50)]
        public string City { get; set; }

        [Required]
        [MaxLength(100)]
        public string Address { get; set; }
        public string? CardCode { get; set; }
        public decimal TotalFee { get; set; }

        [MaxLength(1000)]
        public string? Notes { get; set; }

        [Display(Name = "Role")]
        public int RoleId { get; set; }
        public string? Role { get; set; }

        [Display(Name = "Created")]
        public DateTime CreatedDateTime { get; set; }

        [Display(Name = "Modified")]
        public DateTime ModifiedDateTime { get; set; }

        [Display(Name = "CreatedBy")]
        public string? CreatedByUser { get; set; }

        [Display(Name = "ModifiedBy")]
        public string? ModifiedByUser { get; set; }

        [Display(Name = "CreatedBy")]
        public int? CreatedByUserId { get; set; }

        [Display(Name = "ModifiedBy")]
        public int? ModifiedByUserId { get; set; }

        private const string DocumentIdPattern = @"^\d{13}$";

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            if (!Regex.IsMatch(DocumentId, DocumentIdPattern))
            {
                validationResults.Add(new ValidationResult(
                    "Invalid DocumentId format.",
                    new[] { nameof(DocumentId) }));

                return validationResults;
            }

            return validationResults;
        }
    }
}
