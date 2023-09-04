using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LibApp.WebApp.ViewModels
{
    public class BookViewModel
    {
        //TODO: String nullable maybe not needed?
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Isbn { get; set; }
        public string Edition { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0}", ApplyFormatInEditMode = true)]
        public int ReleaseYear { get; set; }
        public int? PublisherId { get; set; }
        public int? CategoryId { get; set; }
        public int? DepartmentId { get; set; }
        public int? LanguageId { get; set; }
        public string? Publisher { get; set; }
        public string? Category { get; set; }
        public string? Department { get; set; }
        public string? Language { get; set; }
        public ICollection<int>? AuthorIds { get; set; }
        public decimal? Cost { get; set; }
        [Display(Name = "Available")]
        public bool IsAvailable { get; set; }
        public int Quantity { get; set; }
        [Display(Name = "Available Quantity")]
        public int AvailableQuantity { get; set; }
        [Display(Name = "Reserved Quantity")]
        public int ReservedQuantity { get; set; }

        public SelectList? Publishers { get; set; }
        public SelectList? Categories { get; set; }
        public SelectList? Departments { get; set; }
        public SelectList? Languages { get; set; }
        public IEnumerable<(int AuthorId, string AuthorName)>? Authors { get; set; }
        [Display(Name = "CreatedDT")]
        public DateTime CreatedDateTime { get; set; }
        [Display(Name = "ModifiedDT")]
        public DateTime ModifiedDateTime { get; set; }
        [Display(Name = "CreatedBy")]
        public string? CreatedByUser { get; set; }
        [Display(Name = "ModifiedBy")]
        public string? ModifiedByUser { get; set; }
    }
}
