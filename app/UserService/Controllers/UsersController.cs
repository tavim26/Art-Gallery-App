using Microsoft.AspNetCore.Mvc;
using UserService.Domain;
using UserService.Services;
using System.Text;
using UserService.Infrastructure;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UsersService _usersService;

        public UsersController(UserDAO userDAO)
        {
            _usersService = new UsersService(userDAO);
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return _usersService.GetUsers();
        }

        [HttpGet("{id:int}")]
        public ActionResult<User?> GetUserById(int id)
        {
            var user = _usersService.GetUserById(id);
            if (user == null)
                return NotFound();
            return user;
        }

        [HttpPost]
        public ActionResult<User> CreateUser(User user)
        {
            bool result = _usersService.InsertUser(user);
            if (result)
                return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user); // Returnăm utilizatorul creat
            return BadRequest();
        }

        [HttpPut]
        public ActionResult UpdateUser(User user)
        {
            bool result = _usersService.UpdateUser(user);
            if (result)
                return Ok();
            return BadRequest();
        }

        [HttpDelete("{id:int}")]
        public ActionResult DeleteUser(int id)
        {
            bool result = _usersService.DeleteUser(id);
            if (result)
                return Ok();
            return BadRequest();
        }

        [HttpGet("filterByRole")]
        public ActionResult<IEnumerable<User>> FilterUsersByRole([FromQuery] string role)
        {
            return _usersService.FilterUsersByRole(role);
        }

        [HttpGet("export/csv")]
        public IActionResult ExportUsersCsv()
        {
            var users = _usersService.GetUsers();
            var builder = new StringBuilder();
            builder.AppendLine("Id,Name,Role,Phone");

            foreach (var user in users)
            {
                builder.AppendLine($"{user.Id},{user.Name},{user.Role},{user.Phone}");
            }

            return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "users.csv");
        }




        [HttpPost("login")]
        public ActionResult<User?> LogIn([FromQuery] string email, [FromQuery] string passwordHash)
        {
            var user = _usersService.LogIn(email, passwordHash);
            if (user == null)
                return Unauthorized("Invalid credentials");
            return user;
        }

    }
}
