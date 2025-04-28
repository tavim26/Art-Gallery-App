namespace ArtistService.Domain
{
    public class ArtistID
    {
        private int _artistId;

        public ArtistID()
        {
            this._artistId = 0;
        }

        public ArtistID(int artistId)
        {
            this._artistId = artistId;
        }

        public ArtistID(ArtistID artistID)
        {
            this._artistId = artistID._artistId;
        }

        public int ArtistId
        {
            get { return this._artistId; }
            set { this._artistId = value; }
        }
    }
}