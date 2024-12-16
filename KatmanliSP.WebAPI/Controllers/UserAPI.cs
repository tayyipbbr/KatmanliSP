using KatmanliSP.Core.DTOs.ProductDTO;
using KatmanliSP.Core.DTOs.UserDTO;
using KatmanliSP.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KatmanliSP.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAPI : ControllerBase
    {
        private readonly UserService _userService;

        public UserAPI(UserService userService)
        {

            _userService = userService;
        }

        [HttpPost]
        public void CreateCategory(CreateUserDTO createUserDTO)
        {
            _userService.AddUser(createUserDTO);
        }
    }
}
