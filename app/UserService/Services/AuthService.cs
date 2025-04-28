using UserService.Domain;
using UserService.Domain.Contracts;

namespace UserService.Services
{
    public class AuthService
    {
        private IAuthDAO _authDAO;

        public AuthService(IAuthDAO authDAO)
        {
            this._authDAO = authDAO;
        }

        public bool SignUp(Auth auth)
        {
            if (auth == null)
                return false;
            return _authDAO.SignUp(auth);
        }

        public Auth? LogIn(string email, string passwordHash)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(passwordHash))
                return null;
            return _authDAO.LogIn(email, passwordHash);
        }

        public bool DeleteAuthByUserId(int userId)
        {
            if (userId <= 0)
                return false;
            return _authDAO.DeleteAuthByUserId(userId);
        }
    }
}
