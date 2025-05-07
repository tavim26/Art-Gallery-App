using System.Text.Json.Serialization;

namespace UserService.Domain.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Role { get; set; } = "";
    }

}