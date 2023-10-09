using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LibApp.WebApp.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Isbn { get; set; }
        public string Edition { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Released")]
        [DisplayFormat(DataFormatString = "{0}", ApplyFormatInEditMode = true)]
        public int ReleaseYear { get; set; }
        [Display(Name = "Publisher")]
        public int? PublisherId { get; set; }
        [Display(Name = "Category")]
        public int? CategoryId { get; set; }
        [Display(Name = "Department")]
        public int? DepartmentId { get; set; }
        [Display(Name = "Language")]
        public int? LanguageId { get; set; }
        public string Publisher { get; set; }
        public string Category { get; set; }
        public string Department { get; set; }
        public string Language { get; set; }
        public IEnumerable<int> AuthorIds { get; set; }
        public decimal? Cost { get; set; }
        [Display(Name = "Available")]
        public bool IsAvailable { get; set; }
        public int Quantity { get; set; }
        [Display(Name = "Available")]
        public int AvailableQuantity { get; set; }
        [Display(Name = "Reserved")]
        public int ReservedQuantity { get; set; }

        public SelectList Publishers { get; set; }
        public SelectList Categories { get; set; }
        public SelectList Departments { get; set; }
        public SelectList Languages { get; set; }
        public IEnumerable<(int AuthorId, string AuthorName)> Authors { get; set; }
        [Display(Name = "Created")]
        public DateTime CreatedDateTime { get; set; }
        [Display(Name = "Modified")]
        public DateTime ModifiedDateTime { get; set; }
        [Display(Name = "CreatedBy")]
        public string CreatedByUser { get; set; }
        [Display(Name = "ModifiedBy")]
        public string ModifiedByUser { get; set; }
    }
}
