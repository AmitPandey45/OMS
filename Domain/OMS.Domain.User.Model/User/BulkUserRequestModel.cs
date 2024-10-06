namespace OMS.Domain.User.Model.User
{
    public class BulkUserRequestModel
    {
        public List<CreateUserRequestModel> CreateUsers { get; set; } = new List<CreateUserRequestModel>();
        public List<UpdateUserRequestModel> UpdateUsers { get; set; } = new List<UpdateUserRequestModel>();
        public List<string> DeleteUserIds { get; set; } = new List<string>(); // List of User IDs for deletion
    }
}
