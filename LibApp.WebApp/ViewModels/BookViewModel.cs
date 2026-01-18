using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace LibApp.WebApp.ViewModels;

public class BookViewModel : IValidatableObject
{
    public int Id { get; init; }

    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = null!;

    [MaxLength(1000)]
    public string? Description { get; set; }

    [Required]
    [MaxLength(17)]
    public string Isbn { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string Edition { get; set; } = null!;

    [Display(Name = "Released")]
    [Range(-3000, 3000)]
    public int ReleaseYear { get; set; }

    [Display(Name = "Publisher")]
    public int? PublisherId { get; set; }

    [Display(Name = "Category")]
    public int? CategoryId { get; set; }

    [Display(Name = "Department")]
    public int? DepartmentId { get; set; }

    [Display(Name = "Language")]
    public int? LanguageId { get; set; }

    [Display(Name = "Authors")]
    public IEnumerable<(int AuthorId, string AuthorName)>? Authors { get; set; }

    [Display(Name = "Authors")]
    public IEnumerable<int>? AuthorIds { get; set; }

    public string? Publisher { get; set; }
    public string? Category { get; set; }
    public string? Department { get; set; }
    public string? Language { get; set; }

    [Display(Name = "New Author")]
    [MaxLength(100)]
    public string? NewAuthor { get; set; }

    [Range(0, Double.MaxValue)]
    public decimal? Cost { get; set; }

    [Display(Name = "Available")]
    public bool IsAvailable { get; set; }

    [Range(0, 1000)]
    public int Quantity { get; set; }

    [Display(Name = "Available")]
    [Range(0, 1000)]
    public int AvailableQuantity { get; set; }

    [Display(Name = "Reserved")]
    [Range(0, 1000)]
    public int ReservedQuantity { get; set; }

    public string? ImagePath { get; set; }
    public IFormFile? ImageFile { get; set; }

    [Display(Name = "Created")]
    public DateTime CreatedDateTime { get; init; }

    [Display(Name = "Modified")]
    public DateTime ModifiedDateTime { get; set; }

    [Display(Name = "CreatedBy")]
    public string? CreatedByUser { get; set; }

    [Display(Name = "ModifiedBy")]
    public string? ModifiedByUser { get; set; }
    public int? CreatedByUserId { get; init; }
    public int? ModifiedByUserId { get; set; }

    private const string IsbnPattern = @"^(?=.*\d)(?=.*-).+$";

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var validationResults = new List<ValidationResult>();

        if (NewAuthor.IsNullOrEmpty() && AuthorIds.IsNullOrEmpty())
        {
            validationResults.Add(new ValidationResult(
                "Both Author and New Author fields are empty. Chose Authors from the list or enter a new one",
                new[] { nameof(AuthorIds) }));
        }

        if (!Regex.IsMatch(Isbn, IsbnPattern))
        {
            validationResults.Add(new ValidationResult(
                "Invalid ISBN format.",
                new[] { nameof(Isbn) }));

            return validationResults;
        }

        return validationResults;
    }
}