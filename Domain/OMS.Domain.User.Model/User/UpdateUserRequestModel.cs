using System.ComponentModel.DataAnnotations;

namespace OMS.Domain.User.Model.User
{
    public class UpdateUserRequestModel : BaseModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
