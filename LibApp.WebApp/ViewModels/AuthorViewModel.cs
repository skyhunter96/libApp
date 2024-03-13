using System.ComponentModel.DataAnnotations;

namespace LibApp.WebApp.ViewModels
{
    public class AuthorViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

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
