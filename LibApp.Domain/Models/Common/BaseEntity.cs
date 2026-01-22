namespace LibApp.Domain.Models.Common;

public abstract class BaseEntity
{
    public int Id { get; init; }
    public DateTime CreatedDateTime { get; init; }
    public DateTime ModifiedDateTime { get; protected set; }
    public int? CreatedByUserId { get; protected set; }
    public int? ModifiedByUserId { get; protected set; }

    public virtual User? CreatedByUser { get; init; }
    public virtual User? ModifiedByUser { get; protected set; }

    public void SetModifiedDateTime(DateTime dt) => ModifiedDateTime = dt;
    public void SetCreatedByUserId(int? userId)
    {
        CreatedByUserId = userId;
    }
    public void SetModifiedByUserId(int? userId)
    {
        ModifiedByUserId = userId;
    }
}