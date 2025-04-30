using System.ComponentModel.DataAnnotations;

namespace UserService.Domain
{
    public class Auth
    {
        private int _id;
        private int _userId;
        private string _email;
        private string _passwordHash;

        public Auth()
        {
            this._id = 0;
            this._userId = 0;
            this._email = "";
            this._passwordHash = "";
        }

        public Auth(int id, int userId, string? email, string? passwordHash)
        {
            this._id = id;
            this._userId = userId;
            this._email = email ?? "";
            this._passwordHash = passwordHash ?? "";
        }

        public Auth(Auth auth)
        {
            this._id = auth._id;
            this._userId = auth._userId;
            this._email = auth._email;
            this._passwordHash = auth._passwordHash;
        }

        public int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        public int UserId
        {
            get { return this._userId; }
            set { this._userId = value; }
        }



        [Required]
        [EmailAddress]
        public string Email
        {
            get { return this._email; }
            set { this._email = value; }
        }

        [Required]
        [MinLength(6)]
        public string PasswordHash
        {
            get { return this._passwordHash; }
            set { this._passwordHash = value; }
        }
    }
}