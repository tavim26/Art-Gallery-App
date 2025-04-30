namespace UserService.Domain
{
    public class User
    {
        private int _id;
        private string _name;
        private string _role;
        private string _phone;

        public User()
        {
            this._id = 0;
            this._name = "";
            this._role = "";
            this._phone = "";
        }

        public User(int id, string? name, string? role, string? phone)
        {
            this._id = id;
            this._name = name ?? "";
            this._role = role ?? "";
            this._phone = phone ?? "";
        }


        public User(User user)
        {
            this._id = user._id;
            this._name = user._name;
            this._role = user._role;
            this._phone = user._phone;
        }

        public int Id
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
