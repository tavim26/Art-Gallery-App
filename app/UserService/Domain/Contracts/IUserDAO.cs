namespace UserService.Domain.Contracts
{
    public interface IUserDAO
    {
        List<User> GetUsers();
        User? GetUserById(int id);
        bool InsertUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(int id);
        List<User> FilterUsersByRole(string role);
    }
}