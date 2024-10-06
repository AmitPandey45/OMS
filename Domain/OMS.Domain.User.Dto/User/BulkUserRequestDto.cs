namespace OMS.Domain.User.Dto.User
{
    public class BulkUserRequestDto
    {
        public List<CreateUserRequestDto> CreateUsers { get; set; } = new List<CreateUserRequestDto>();
        public List<UpdateUserRequestDto> UpdateUsers { get; set; } = new List<UpdateUserRequestDto>();
        public List<int> DeleteUserIds { get; set; } = new List<int>();
    }
}
