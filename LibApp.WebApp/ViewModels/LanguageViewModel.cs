using System.ComponentModel.DataAnnotations;

namespace LibApp.WebApp.ViewModels
{
    public class LanguageViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
