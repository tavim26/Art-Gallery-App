using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Generators;
using UserService.Domain;
using UserService.Infrastructure;
using UserService.Services;
using BCrypt.Net;


namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private AuthService _authService;
        private readonly JwtTokenGenerator _jwtGenerator;


        public AuthController(AuthDAO authDAO, JwtTokenGenerator jwtGenerator)
        {
            _authService = new AuthService(authDAO);
            _jwtGenerator = jwtGenerator;
        }

        [HttpPost("signup")]
        public ActionResult SignUp([FromBody] Auth auth)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool result = _authService.SignUp(auth);
            if (result)
                return Ok();
            return BadRequest("Register failed.");
        }




        [HttpPost("login")]
        public ActionResult<string> LogIn([FromQuery] string email, [FromQuery] string password)
        {
            var auth = _authService.GetAuthByEmail(email);
            if (auth == null)
                return Unauthorized("Email does not exist.");

            bool verified = BCrypt.Net.BCrypt.Verify(password, auth.PasswordHash);
            if (!verified)
                return Unauthorized("Incorrect password.");


            string role = "User";
            string token = _jwtGenerator.GenerateToken(email, role);

            return Ok(token);
        }


    }
}