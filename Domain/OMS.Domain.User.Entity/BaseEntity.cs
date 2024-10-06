namespace OMS.Domain.User.Entity
{
    public class BaseEntity
    {
        public Guid? GuidId { get; set; }
        public int CreatedBy { get; set; } = -1;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public int ModifiedBy { get; set; } = -1;
        public DateTime? ModifiedDate { get; set; } = DateTime.UtcNow;
    }
}
