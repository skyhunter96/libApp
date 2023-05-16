using Domain.Models.Common;

namespace Domain.Models;

public class Log
{
    public int Id { get; set; }
    public LogType LogType { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DateTime { get; set; }
    public int? UserId { get; set; }
    public string StackTrace { get; set; }

    public virtual User User { get; set; }
}
