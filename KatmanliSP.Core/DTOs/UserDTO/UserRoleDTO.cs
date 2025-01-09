using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KatmanliSP.Core.DTOs.UserDTO
{
    public class UserRoleDTO
    {
        public int RoleId { get; set; }
        public string Rolename { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
    }
}
