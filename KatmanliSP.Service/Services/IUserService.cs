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
        IResponse<OnlineUserDTO> Login(LoginUserDTO loginUserDTO);
        List<char> GetUserRoles(int userId); // Class patlamasın diye char yaptım. DEBUG için.
    }
}
