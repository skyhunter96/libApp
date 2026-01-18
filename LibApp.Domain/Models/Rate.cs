using LibApp.Domain.Models.Common;

namespace LibApp.Domain.Models;

public sealed class Rate : BaseEntity
{
    public decimal RateFee { get; set; }
    public int? ApplyAfterDays { get; set; }
}
