using System.ComponentModel.DataAnnotations;

namespace LibApp.WebApp.ViewModels;

public class AuthorViewModel
{
    public int Id { get; init; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;

    [Display(Name = "Created")]
    public DateTime CreatedDateTime { get; init; }

    [Display(Name = "Modified")]
    public DateTime ModifiedDateTime { get; set; }

    [Display(Name = "CreatedBy")]
    public string? CreatedByUser { get; init; }

    [Display(Name = "ModifiedBy")]
    public string? ModifiedByUser { get; set; }

    public int? CreatedByUserId { get; init; }
    public int? ModifiedByUserId { get; set; }
}