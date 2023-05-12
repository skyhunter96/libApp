using Domain.Models.Common;

namespace Domain.Models;

public class Publisher : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
}
