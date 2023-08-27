using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Domain.Models.Common
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        [Display(Name = "CreatedDT")]
        public DateTime CreatedDateTime { get; set; }
        [Display(Name = "ModifiedDT")]
        public DateTime ModifiedDateTime { get; set; }
        [Display(Name = "CreatedBy")]
        public int CreatedByUserId { get; set; }
        [Display(Name = "ModifiedBy")]
        public int ModifiedByUserId { get; set; }

        //public virtual User CreatedByUser { get; set; }
        //public virtual User ModifiedByUser { get; set; }
    }
}