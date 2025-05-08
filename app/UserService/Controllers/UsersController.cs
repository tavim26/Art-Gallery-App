using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using UserService.Domain;
using UserService.Domain.DTO;
using UserService.Domain.Mappers;
using UserService.Infrastructure;
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
            var result = _usersService.InsertUser(user);

            if (!result)
                return BadRequest("Insert failed.");

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, UserMapper.ToDTO(user));
        }

        [HttpPut]
        public ActionResult UpdateUser([FromBody] UserDTO dto)
        {
            var existing = _usersService.GetUserById(dto.Id);
            if (existing == null)
                return NotFound("User not found.");

           
            var user = UserMapper.FromDTO(dto, existing.PasswordHash);

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
            var users = _usersService.GetUsers();
            var builder = new StringBuilder();
            builder.AppendLine("Id,Name,Role,Phone");

            foreach (var user in users)
                builder.AppendLine($"{user.Id},{user.Name},{user.Role},{user.Phone}");

            return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "users.csv");
        }

        [HttpPost("login")]
        public ActionResult<UserDTO> LogIn([FromBody] UserLoginDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest("Email and password required.");

            var hash = HashHelper.HashPassword(dto.Password);
            var user = _usersService.LogIn(dto.Email, hash);

            if (user == null)
                return Unauthorized("Invalid credentials.");

            return Ok(UserMapper.ToDTO(user));
        }
    }
}
