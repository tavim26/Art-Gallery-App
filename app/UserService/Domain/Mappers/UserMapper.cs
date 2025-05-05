using UserService.Domain.DTO;

namespace UserService.Domain.Mappers
{
    public static class UserMapper
    {
        public static UserDTO ToDTO(User user) => new UserDTO
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role,
            Phone = user.Phone
        };

        public static User FromDTO(UserDTO dto, string passwordHash) => new User
        {
            Id = dto.Id,
            Name = dto.Name,
            Email = dto.Email,
            Phone = dto.Phone,
            Role = dto.Role,
            PasswordHash = passwordHash
        };
    }
}