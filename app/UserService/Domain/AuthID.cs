namespace UserService.Domain
{
    public class AuthID
    {
        private int _authId;

        public AuthID()
        {
            this._authId = 0;
        }

        public AuthID(int authId)
        {
            this._authId = authId;
        }

        public AuthID(AuthID authId)
        {
            this._authId = authId._authId;
        }

        public int Id
        {
            get { return this._authId; }
            set { this._authId = value; }
        }
    }
}
