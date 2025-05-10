using Microsoft.AspNetCore.Mvc;
using UserService.Domain.DTO;
using UserService.Domain.Mappers;
using UserService.Services;
using UserService.Services.Notifications;

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
        public ActionResult<IEnumerable<UserDTO>> GetUsers()
        {
            var users = _usersService.GetUserDTOs();
            return Ok(users);
        }

        [HttpGet("{id:int}")]
        public ActionResult<UserDTO> GetUserById(int id)
        {
            var dto = _usersService.GetUserDTO(id);
            return dto == null ? NotFound() : Ok(dto);
        }

        [HttpPost]
        public ActionResult<UserDTO> CreateUser([FromBody] UserDTO dto)
        {
            var result = _usersService.CreateUser(dto);
            if (!result.success)
                return Conflict(result.errorMessage);

            return CreatedAtAction(nameof(GetUserById), new { id = result.user!.Id }, result.user);
        }

        [HttpPut]
        public ActionResult UpdateUser([FromBody] UserDTO dto)
        {
            var result = _usersService.UpdateUser(dto);
            if (!result.success)
                return NotFound(result.errorMessage);

            _notificationService.NotifyByEmail(dto.Email, "Your account details have been modified.");
            _notificationService.NotifyBySms(dto.Phone, "Your account details have been modified.");
            return Ok("User updated and notified.");
        }

        [HttpDelete("{id:int}")]
        public ActionResult DeleteUser(int id)
        {
            return _usersService.DeleteUser(id)
                ? Ok("User deleted.")
                : BadRequest("Delete failed.");
        }

        [HttpGet("filterByRole")]
        public ActionResult<IEnumerable<UserDTO>> FilterUsersByRole([FromQuery] string role)
        {
            var dtos = _usersService.FilterUsersByRoleDTO(role);
            return Ok(dtos);
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
            var result = _usersService.TryLogIn(dto.Email, dto.Password);
            if (!result.success || result.user == null)
                return Unauthorized(result.errorMessage);

            return Ok(result.user);
        }
    }
}
