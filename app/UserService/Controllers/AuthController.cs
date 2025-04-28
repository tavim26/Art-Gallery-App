using Microsoft.AspNetCore.Mvc;
using UserService.Domain;
using UserService.Infrastructure;
using UserService.Services;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private AuthService _authService;

        public AuthController(AuthDAO authDAO)
        {
            _authService = new AuthService(authDAO);
        }

        [HttpPost("signup")]
        public ActionResult SignUp(Auth auth)
        {
            bool result = _authService.SignUp(auth);
            if (result)
                return Ok();
            return BadRequest();
        }

        [HttpPost("login")]
        public ActionResult<Auth?> LogIn([FromQuery] string email, [FromQuery] string passwordHash)
        {
            var auth = _authService.LogIn(email, passwordHash);
            if (auth == null)
                return Unauthorized();
            return auth;
        }
    }
}