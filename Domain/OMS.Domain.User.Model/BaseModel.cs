namespace OMS.Domain.User.Model
{
    public class BaseModel
    {
        public Guid? GuidId { get; set; }
        public int CreatedBy { get; set; } = -1;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public int ModifiedBy { get; set; } = -1;
        public DateTime? ModifiedDate { get; set; } = DateTime.UtcNow;
    }
}
