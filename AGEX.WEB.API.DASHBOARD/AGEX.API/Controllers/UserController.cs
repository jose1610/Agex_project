using AGEX.CORE.Dtos.Login.Enter;
using AGEX.CORE.Dtos.Login.Get;
using AGEX.CORE.Dtos.Login.Register;
using AGEX.CORE.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace AGEX.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Route(nameof(Login)), HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginReqDto request)
        {
            var dt = await _userService.Login(request);

            return Ok(dt);
        }

        [Route(nameof(GetUsers)), HttpPost]
        public async Task<IActionResult> GetUsers([FromBody] GetUsersReqDto request)
        {
            var dt = await _userService.GetUser(request);

            return Ok(dt);
        }

        [Route(nameof(RegisterUser)), HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserReqDto request)
        {
            var dt = await _userService.RegisterUser(request);

            return Ok(dt);
        }
    }
}
