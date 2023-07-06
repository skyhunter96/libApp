using Domain.Models.Common;

namespace Domain.Models;

public class Rate : BaseEntity
{
    public decimal RateFee { get; set; }
    public int? ApplyAfterDays { get; set; }
}
