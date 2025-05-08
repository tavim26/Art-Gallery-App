using Microsoft.AspNetCore.Mvc;
using System.Text;
using UserService.Domain;
using UserService.Domain.DTO;
using UserService.Domain.Mappers;
using UserService.Services;
using UserService.Services.Notifications;
using UserService.Utils;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _usersService;
        private readonly NotificationService _notificationService;

        public UsersController(UsersService usersService, NotificationService notificationService)
        {
            _usersService = usersService;
            _notificationService = notificationService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return Ok(_usersService.GetUsers());
        }

        [HttpGet("{id:int}")]
        public ActionResult<User?> GetUserById(int id)
        {
            var user = _usersService.GetUserById(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public ActionResult<UserDTO> CreateUser([FromBody] UserDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest("Email and password are required.");

            var user = UserMapper.FromDTO(dto, HashHelper.HashPassword(dto.Password));
            var success = _usersService.InsertUser(user);

            if (!success)
                return Conflict("A user with this email already exists.");

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, UserMapper.ToDTO(user));
        }


        [HttpPut]
        public ActionResult UpdateUser([FromBody] UserDTO dto)
        {
            var existing = _usersService.GetUserById(dto.Id);
            if (existing == null)
                return NotFound("User not found.");

            var passwordHash = string.IsNullOrWhiteSpace(dto.Password)
                ? existing.PasswordHash
                : HashHelper.HashPassword(dto.Password);

            var user = UserMapper.FromDTO(dto, passwordHash);

            bool result = _usersService.UpdateUser(user);

            if (result)
            {
                _notificationService.NotifyByEmail(user.Email, "Your account details have been modified.");
                _notificationService.NotifyBySms(user.Phone, "Your account details have been modified.");
                return Ok("User updated and notified.");
            }

            return BadRequest("Update failed.");
        }



        [HttpDelete("{id:int}")]
        public ActionResult DeleteUser(int id)
        {
            bool result = _usersService.DeleteUser(id);
            return result ? Ok("User deleted.") : BadRequest("Delete failed.");
        }


        [HttpGet("filterByRole")]
        public ActionResult<IEnumerable<User>> FilterUsersByRole([FromQuery] string role)
        {
            var users = _usersService.FilterUsersByRole(role);
            return Ok(users);
        }


        [HttpGet("export/csv")]
        public IActionResult ExportUsersCsv()
        {
            var export = _usersService.ExportUsersCsv();
            return File(export.content, export.contentType, export.fileName);
        }



        [HttpPost("login")]
        public ActionResult<UserDTO> LogIn([FromBody] UserLoginDTO dto)
        {
            var hash = HashHelper.HashPassword(dto.Password);
            var result = _usersService.TryLogIn(dto.Email, hash);

            if (result == null || result.Value.user == null)
                return Unauthorized(result?.error ?? "Login failed.");

            return Ok(UserMapper.ToDTO(result.Value.user));
        }

    }
}
