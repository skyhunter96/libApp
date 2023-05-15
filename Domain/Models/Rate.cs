using Domain.Models.Common;

namespace Domain.Models;

public class Rate : BaseEntity
{
    public virtual decimal RateFee { get; set; }
}
