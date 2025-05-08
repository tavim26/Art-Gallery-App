using System.Text;
using UserService.Domain;
using UserService.Domain.Contracts;
using UserService.Services.Exports;

namespace UserService.Services
{
    public class UsersService
    {
        private readonly IUserDAO _userDAO;


        public UsersService(IUserDAO userDAO)
        {
            this._userDAO = userDAO;
        }

        public List<User> GetUsers()
        {
            return _userDAO.GetUsers();
        }

        public User? GetUserById(int id)
        {
            if (id <= 0)
                return null;
            return _userDAO.GetUserById(id);
        }

        public bool InsertUser(User user)
        {
            if (user == null || string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Role)
                || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.PasswordHash))
                return false;

            var existing = _userDAO.GetUserByEmail(user.Email);
            if (existing != null)
                return false;

            return _userDAO.InsertUser(user);
        }


        public bool UpdateUser(User user)
        {
            if (user == null)
                return false;
            return _userDAO.UpdateUser(user);
        }

        public bool DeleteUser(int id)
        {
            if (id <= 0)
                return false;
            return _userDAO.DeleteUser(id);
        }

        public List<User> FilterUsersByRole(string role)
        {
            if (string.IsNullOrEmpty(role))
                return new List<User>();
            return _userDAO.FilterUsersByRole(role);
        }

        public User? GetUserByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return null;
            return _userDAO.GetUserByEmail(email);
        }

        public (User? user, string error)? TryLogIn(string email, string passwordHash)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(passwordHash))
                return (null, "Email and password are required.");

            var existingUser = _userDAO.GetUserByEmail(email);
            if (existingUser == null)
                return (null, "No account found with this email.");

            if (existingUser.PasswordHash != passwordHash)
                return (null, "Incorrect password.");

            return (existingUser, "");
        }



        public (byte[] content, string contentType, string fileName) ExportUsersCsv()
        {
            var users = GetUsers();
            var strategy = new CsvExportStrategy();
            var data = strategy.Export(users);
            var bytes = Encoding.UTF8.GetBytes(data);
            var filename = $"users_{DateTime.Now:yyyyMMdd_HHmmss}{strategy.FileExtension}";

            return (bytes, strategy.ContentType, filename);
        }


    }
}