using KatmanliSP.Core.DTOs.UserDTO;
using KatmanliSP.Service.Services;
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

        [HttpPost("Login")]
        public IActionResult Login(LoginUserDTO loginUserDTO)
        {
            // userId lazım olursa eğer success içerisine ekle.
            var response = _userService.Login(loginUserDTO); // token

            if (response.Issuccess)
            {
                return Ok("Giriş başarılı.");
            }
            return BadRequest(response.Message);                                    // TODO: hata-false'da olumlu mesaj dönüyor
        }

        [HttpPost("GetUserRoles")]
        public void GetUserRoles(int userId)
        {
            //var response = _userService.GetUserRoles(userId);

            //if (response.Issuccess)
            //{
            //    return Ok(response);
            //}
            //return BadRequest(response.Message);
        }


        [HttpPost("Register")]
        public IActionResult Register(CreateUserDTO createUserDTO)
        {
            var response = _userService.Register(createUserDTO);

            if (response.Issuccess)
            {
                return Ok("Kullanıcı başarı ile oluşturuldu.");
            }
            return BadRequest(response.Message);
        }
    }
}
