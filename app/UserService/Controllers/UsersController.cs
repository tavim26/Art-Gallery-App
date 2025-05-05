using Microsoft.AspNetCore.Mvc;
using UserService.Domain;
using UserService.Services;
using System.Text;
using UserService.Infrastructure;
using UserService.Domain.DTO;
using UserService.Domain.Mappers;
using UserService.Utils;

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
        public ActionResult<UserDTO> CreateUser([FromBody] UserDTO dto)
        {
            // Validare minimă
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest("Email and password are required.");

            // Hash-uim parola și construim modelul User
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

            string hashToUse = string.IsNullOrWhiteSpace(dto.Password)
                ? existing.PasswordHash
                : HashHelper.HashPassword(dto.Password);

            var user = UserMapper.FromDTO(dto, hashToUse);

            bool result = _usersService.UpdateUser(user);
            return result ? Ok() : BadRequest("Update failed.");
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
