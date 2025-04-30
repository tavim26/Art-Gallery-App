namespace UserService.Domain.Contracts
{
    public interface IAuthDAO
    {
        bool SignUp(Auth auth);
        Auth? LogIn(string email, string passwordHash);
        bool DeleteAuthByUserId(int userId);

        public Auth? GetAuthByEmail(string email);
    }
}
