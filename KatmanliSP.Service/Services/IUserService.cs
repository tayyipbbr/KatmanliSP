using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KatmanliSP.Core.ResponseMessages;
using KatmanliSP.Core.DTOs.UserDTO;

namespace KatmanliSP.Service.Services
{
    public interface IUserService
    {
        IResponse<UserRoleDTO> Login(LoginUserDTO loginUserDTO);
        public List<UserRoleDTO> GetUserRoles(int userId);
    }
}
