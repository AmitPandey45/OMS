using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Domain.User.Model.User
{
    public class GetAllUsersResponseModel
    {
        public List<GetUserResponseModel> Users { get; set; } = new List<GetUserResponseModel>();
        public int TotalCount { get; set; }
    }
}
