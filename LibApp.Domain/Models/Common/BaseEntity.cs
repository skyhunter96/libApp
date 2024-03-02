namespace Domain.Models.Common
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int? CreatedByUserId { get; set; }
        public int? ModifiedByUserId { get; set; }

        public virtual User? CreatedByUser { get; set; }
        public virtual User? ModifiedByUser { get; set; }
    }
}