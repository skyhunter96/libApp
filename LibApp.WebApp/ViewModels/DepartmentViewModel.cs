using System.ComponentModel.DataAnnotations;

namespace LibApp.WebApp.ViewModels;

//TODO: Refactor encapsulation
public class DepartmentViewModel
{
    public int Id { get; init; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;

    [MaxLength(1000)]
    public string Description { get; set; } = null!;

    [MaxLength(100)]
    public string? Location { get; set; }
    public decimal? Budget { get; set; }

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