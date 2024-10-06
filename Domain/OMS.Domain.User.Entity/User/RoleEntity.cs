namespace OMS.Domain.User.Entity.User
{
    public class RoleEntity
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public List<UserEntity> Users { get; set; } = new List<UserEntity>();
    }
}
