using Microsoft.AspNetCore.Mvc;
using UserService.Domain.DTO;
using UserService.Services.Facade;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersFacade _facade;

        public UsersController(UsersFacade facade)
        {
            _facade = facade;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> GetUsers()
        {
            return Ok(_facade.GetAll());
        }

        [HttpGet("{id:int}")]
        public ActionResult<UserDTO> GetUserById(int id)
        {
            var dto = _facade.GetById(id);
            return dto == null ? NotFound() : Ok(dto);
        }

        [HttpPost]
        public ActionResult<UserDTO> CreateUser([FromBody] UserDTO dto)
        {
            var result = _facade.Create(dto);
            if (!result.success)
                return Conflict(result.errorMessage);

            return CreatedAtAction(nameof(GetUserById), new { id = result.user!.Id }, result.user);
        }

        [HttpPut]
        public ActionResult UpdateUser([FromBody] UserDTO dto)
        {
            var result = _facade.Update(dto);
            return result.success
                ? Ok("User updated and notified.")
                : NotFound(result.errorMessage);
        }

        [HttpDelete("{id:int}")]
        public ActionResult DeleteUser(int id)
        {
            return _facade.Delete(id)
                ? Ok("User deleted.")
                : BadRequest("Delete failed.");
        }

        [HttpGet("filterByRole")]
        public ActionResult<IEnumerable<UserDTO>> FilterUsersByRole([FromQuery] string role)
        {
            return Ok(_facade.FilterByRole(role));
        }

        [HttpGet("export/csv")]
        public IActionResult ExportUsersCsv()
        {
            var export = _facade.ExportCsv();
            return File(export.content, export.contentType, export.fileName);
        }

        [HttpPost("login")]
        public ActionResult<UserDTO> LogIn([FromBody] UserLoginDTO dto)
        {
            var result = _facade.Login(dto.Email, dto.Password);
            if (!result.success || result.user == null)
                return Unauthorized(result.errorMessage);

            return Ok(result.user);
        }
    }
}
