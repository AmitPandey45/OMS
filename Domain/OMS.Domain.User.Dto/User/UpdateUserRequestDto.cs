namespace OMS.Domain.User.Dto.User
{
    public class UpdateUserRequestDto : BaseDto
    {
        public int UserId { get; set; } = -1;
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
