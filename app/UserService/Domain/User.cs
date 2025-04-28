namespace UserService.Domain
{
    public class User
    {
        private UserID _id;
        private string _name;
        private string _role;
        private string _phone;

        public User()
        {
            this._id = new UserID();
            this._name = "";
            this._role = "";
            this._phone = "";
        }

        public User(UserID id, string? name, string? role, string? phone)
        {
            this._id = id;
            this._name = name ?? "";
            this._role = role ?? "";
            this._phone = phone ?? "";
        }

        public User(int userId, string? name, string? role, string? phone)
        {
            this._id = new UserID(userId);
            this._name = name ?? "";
            this._role = role ?? "";
            this._phone = phone ?? "";
        }

        public User(User user)
        {
            this._id = new UserID(user._id);
            this._name = user._name;
            this._role = user._role;
            this._phone = user._phone;
        }

        public UserID Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        public string Role
        {
            get { return this._role; }
            set { this._role = value; }
        }

        public string Phone
        {
            get { return this._phone; }
            set { this._phone = value; }
        }
    }
}
