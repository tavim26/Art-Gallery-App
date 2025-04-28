namespace UserService.Domain
{
    public class UserID
    {
        private int _userId;

        public UserID()
        {
            this._userId = 0;
        }

        public UserID(int userId)
        {
            this._userId = userId;
        }

        public UserID(UserID userId)
        {
            this._userId = userId._userId;
        }

        public int Id
        {
            get { return this._userId; }
            set { this._userId = value; }
        }
    }
}