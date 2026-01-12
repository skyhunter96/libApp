using System.ComponentModel.DataAnnotations;

namespace LibApp.WebApp.ViewModels;

public class ReservationViewModel
{
    public int Id { get; set; }
    public DateTime? LoanDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ActualReturnDate { get; set; }
    public decimal LateFee { get; set; }

    [Display(Name = "IsStarted")]
    public bool IsStarted { get; set; }

    [Display(Name = "Books")]
    public IEnumerable<(int? BookId, string? BookName)>? Books { get; set; }

    [Display(Name = "Books")]
    public IEnumerable<int>? BookIds { get; set; }

    [Display(Name = "ReservedBy")]
    public string? ReservedByUser { get; set; }

    [Display(Name = "Created")]
    public DateTime CreatedDateTime { get; set; }

    [Display(Name = "Modified")]
    public DateTime ModifiedDateTime { get; set; }

    [Display(Name = "CreatedBy")]
    public string? CreatedByUser { get; set; }

    [Display(Name = "ModifiedBy")]
    public string? ModifiedByUser { get; set; }

    public int? ReservedByUserId { get; set; }
    public int? CreatedByUserId { get; set; }
    public int? ModifiedByUserId { get; set; }
}