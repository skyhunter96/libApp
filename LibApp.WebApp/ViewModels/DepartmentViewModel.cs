using System.ComponentModel.DataAnnotations;

namespace LibApp.WebApp.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [MaxLength(100)]
        public string? Location { get; set; }
        public decimal? Budget { get; set; }

        [Display(Name = "Created")]
        public DateTime CreatedDateTime { get; set; }

        [Display(Name = "Modified")]
        public DateTime ModifiedDateTime { get; set; }

        [Display(Name = "CreatedBy")]
        public string? CreatedByUser { get; set; }

        [Display(Name = "ModifiedBy")]
        public string? ModifiedByUser { get; set; }

        public int? CreatedByUserId { get; set; }
        public int? ModifiedByUserId { get; set; }
    }
}
