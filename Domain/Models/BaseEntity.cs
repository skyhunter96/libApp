namespace Domain.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int CreatedByUserId { get; set; }
        public int ModifiedByUserId { get; set; }
    }
}