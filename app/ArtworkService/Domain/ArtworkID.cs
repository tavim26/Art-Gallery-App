namespace ArtworkService.Domain
{
    public class ArtworkID
    {
        private int _artworkId;

        public ArtworkID()
        {
            this._artworkId = 0;
        }

        public ArtworkID(int artworkId)
        {
            this._artworkId = artworkId;
        }

        public ArtworkID(ArtworkID artworkID)
        {
            this._artworkId = artworkID._artworkId;
        }

        public int ArtworkId
        {
            get { return this._artworkId; }
            set { this._artworkId = value; }
        }
    }
}
