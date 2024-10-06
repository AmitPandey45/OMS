namespace OMS.Domain.User.Entity.User
{
    public class UserProfileEntity
    {
        public string UserProfileId { get; set; }
        public string UserId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public UserEntity User { get; set; }
    }
}
