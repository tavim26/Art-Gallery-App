using UserService.Domain;
using UserService.Domain.Contracts;
using UserService.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace UserService.Infrastructure
{
    public class AuthDAO : DbContext, IAuthDAO
    {
        private DbSet<AuthEntity> _authsSet { get; set; }

        public AuthDAO(DbContextOptions<AuthDAO> options)
            : base(options) { }

        public bool SignUp(Auth auth)
        {
            if (auth == null)
                return false;
            try
            {
                _authsSet.Add(new AuthEntity(auth));
                return SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public Auth? LogIn(string email, string passwordHash)
        {
            try
            {
                var authEntity = _authsSet.FirstOrDefault(a => a.Email == email && a.PasswordHash == passwordHash);
                return authEntity?.ToAuth();
            }
            catch
            {
                return null;
            }
        }

        public bool DeleteAuthByUserId(int userId)
        {
            try
            {
                var authEntity = _authsSet.FirstOrDefault(a => a.UserId == userId);
                if (authEntity != null)
                {
                    _authsSet.Remove(authEntity);
                    return SaveChanges() > 0;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
