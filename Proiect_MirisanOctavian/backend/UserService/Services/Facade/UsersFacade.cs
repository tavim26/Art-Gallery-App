using UserService.Domain.DTO;

namespace UserService.Services.Facade
{
    public class UsersFacade
    {
        private readonly UsersService _usersService;

        public UsersFacade(UsersService usersService)
        {
            _usersService = usersService;
        }

        public IEnumerable<UserDTO> GetAll()
            => _usersService.GetUserDTOs();

        public UserDTO? GetById(int id)
            => _usersService.GetUserDTO(id);

        public (bool success, string? errorMessage, UserDTO? user) Create(UserDTO dto)
        {
            var (success, user, errorMessage) = _usersService.CreateUser(dto);
            return (success, errorMessage, user);
        }

        public (bool success, string? errorMessage) Update(UserDTO dto)
            => _usersService.UpdateUser(dto); 

        public bool Delete(int id)
            => _usersService.DeleteUser(id);

        public IEnumerable<UserDTO> FilterByRole(string role)
            => _usersService.FilterUsersByRoleDTO(role);

        public (byte[] content, string contentType, string fileName) ExportCsv()
            => _usersService.ExportUsersCsv();

        public (bool success, string? errorMessage, UserDTO? user) Login(string email, string password)
        {
            var (success, user, errorMessage) = _usersService.TryLogIn(email, password);
            return (success, errorMessage, user);
        }
    }
}