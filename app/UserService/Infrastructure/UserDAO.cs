using UserService.Domain;
using UserService.Domain.Contracts;
using UserService.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace UserService.Infrastructure
{
    public class UserDAO : DbContext, IUserDAO
    {
        private readonly DbSet<UserEntity> _usersSet;

        public UserDAO(DbContextOptions<UserDAO> options)
            : base(options)
        {
            _usersSet = Set<UserEntity>();
        }


        public List<User> GetUsers()
        {
            try
            {
                List<User> users = new List<User>();
                if (_usersSet != null)
                    foreach (var userEntity in _usersSet)
                        users.Add(userEntity.ToUser());
                return users;
            }
            catch
            {
                return new List<User>();
            }
        }

        public User? GetUserById(int id)
        {
            try
            {
                var userEntity = _usersSet.FirstOrDefault(u => u.Id == id);
                return userEntity?.ToUser();
            }
            catch
            {
                return null;
            }
        }

        public bool InsertUser(User user)
        {
            if (user == null)
                return false;

            try
            {
                var userEntity = new UserEntity(user);
                _usersSet.Add(userEntity);
                int result = SaveChanges();
                Console.WriteLine($"INSERT RESULT: {result}");

                if (result > 0)
                {
                    user.Id = userEntity.Id;
                    return true;
                }

                Console.WriteLine("Insert failed at SaveChanges.");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("INSERT EXCEPTION: " + ex.Message);
                return false;
            }
        }


        public bool UpdateUser(User user)
        {
            if (user == null)
                return false;
            try
            {
                _usersSet.Update(new UserEntity(user));
                return SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteUser(int id)
        {
            try
            {
                var userEntity = _usersSet.FirstOrDefault(u => u.Id == id);
                if (userEntity != null)
                {
                    _usersSet.Remove(userEntity);
                    return SaveChanges() > 0;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public List<User> FilterUsersByRole(string role)
        {
            try
            {
                List<User> users = new List<User>();
                if (_usersSet != null)
                {
                    var query = _usersSet.Where(u => u.Role == role);
                    foreach (var userEntity in query)
                        users.Add(userEntity.ToUser());
                }
                return users;
            }
            catch
            {
                return new List<User>();
            }
        }

        public User? GetUserByEmail(string email)
        {
            try
            {
                var userEntity = _usersSet.FirstOrDefault(u => u.Email == email);
                return userEntity?.ToUser();
            }
            catch
            {
                return null;
            }
        }


        public User? LogIn(string email, string passwordHash)
        {
            try
            {
                var userEntity = _usersSet.FirstOrDefault(u => u.Email == email && u.PasswordHash == passwordHash);
                return userEntity?.ToUser();
            }
            catch
            {
                return null;
            }
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>();
            base.OnModelCreating(modelBuilder);
        }



    }
}
