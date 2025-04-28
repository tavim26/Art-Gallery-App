namespace UserService.Domain
{
    public class Auth
    {
        private AuthID _id;
        private int _userId;
        private string _email;
        private string _passwordHash;

        public Auth()
        {
            this._id = new AuthID();
            this._userId = 0;
            this._email = "";
            this._passwordHash = "";
        }

        public Auth(AuthID id, int userId, string? email, string? passwordHash)
        {
            this._id = id;
            this._userId = userId;
            this._email = email ?? "";
            this._passwordHash = passwordHash ?? "";
        }

        public Auth(int authId, int userId, string? email, string? passwordHash)
        {
            this._id = new AuthID(authId);
            this._userId = userId;
            this._email = email ?? "";
            this._passwordHash = passwordHash ?? "";
        }

        public Auth(Auth auth)
        {
            this._id = new AuthID(auth._id);
            this._userId = auth._userId;
            this._email = auth._email;
            this._passwordHash = auth._passwordHash;
        }

        public AuthID Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        public int UserId
        {
            get { return this._userId; }
            set { this._userId = value; }
        }

        public string Email
        {
            get { return this._email; }
            set { this._email = value; }
        }

        public string PasswordHash
        {
            get { return this._passwordHash; }
            set { this._passwordHash = value; }
        }
    }
}