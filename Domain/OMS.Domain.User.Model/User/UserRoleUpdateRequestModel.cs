using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Domain.User.Model.User
{
    public class UserRoleUpdateRequestModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public List<string> Roles { get; set; } = new List<string>();
    }
}
