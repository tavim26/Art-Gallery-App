using System.Text;
using UserService.Domain;
using UserService.Domain.Contracts;
using UserService.Domain.DTO;
using UserService.Domain.Mappers;
using UserService.Services.Exports;
using UserService.Services.Observers;
using UserService.Utils;

namespace UserService.Services
{
    public class UsersService
    {
        private readonly IUserDAO _userDAO;
        private readonly UserUpdateNotifier _notifier;

        public UsersService(IUserDAO userDAO, UserUpdateNotifier notifier)
        {
            _userDAO = userDAO;
            _notifier = notifier;
        }


        public List<UserDTO> GetUserDTOs()
        {
            return _userDAO.GetUsers().Select(UserMapper.ToDTO).ToList();
        }

        public UserDTO? GetUserDTO(int id)
        {
            var user = GetUserById(id);
            return user == null ? null : UserMapper.ToDTO(user);
        }

        public (bool success, UserDTO? user, string? errorMessage) CreateUser(UserDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
                return (false, null, "Email and password are required.");

            var existing = _userDAO.GetUserByEmail(dto.Email);
            if (existing != null)
                return (false, null, "A user with this email already exists.");

            var user = UserMapper.FromDTO(dto, HashHelper.HashPassword(dto.Password));
            bool success = _userDAO.InsertUser(user);

            return success
                ? (true, UserMapper.ToDTO(user), null)
                : (false, null, "Insert failed.");
        }

        public (bool success, string? errorMessage) UpdateUser(UserDTO dto)
        {
            var user = _userDAO.GetUserById(dto.Id);
            if (user == null)
                return (false, "User not found.");

            user.Name = dto.Name;
            user.Email = dto.Email;
            user.Phone = dto.Phone;
            user.Role = dto.Role;

            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                var hashedPassword = HashHelper.HashPassword(dto.Password);
                user.PasswordHash = hashedPassword;
            }

            var success = _userDAO.UpdateUser(user);
            if (success)
            {
                _notifier.Notify(user);
                return (true, null);
            }

            return (false, "Update failed.");
        }


        public bool DeleteUser(int id) => id > 0 && _userDAO.DeleteUser(id);

        public List<UserDTO> FilterUsersByRoleDTO(string role)
        {
            return _userDAO.FilterUsersByRole(role)
                .Select(UserMapper.ToDTO).ToList();
        }

        public (byte[] content, string contentType, string fileName) ExportUsersCsv()
        {
            var users = _userDAO.GetUsers();
            var strategy = new CsvExportStrategy();
            var data = strategy.Export(users);
            var bytes = Encoding.UTF8.GetBytes(data);
            var filename = $"users_{DateTime.Now:yyyyMMdd_HHmmss}{strategy.FileExtension}";

            return (bytes, strategy.ContentType, filename);
        }

        public (bool success, UserDTO? user, string? errorMessage) TryLogIn(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return (false, null, "Email and password are required.");

            var existingUser = _userDAO.GetUserByEmail(email);
            if (existingUser == null)
                return (false, null, "No account found with this email.");

            var passwordHash = HashHelper.HashPassword(password);
            if (existingUser.PasswordHash != passwordHash)
                return (false, null, "Incorrect password.");

            return (true, UserMapper.ToDTO(existingUser), null);
        }

        private User? GetUserById(int id) => id <= 0 ? null : _userDAO.GetUserById(id);
    }
}
