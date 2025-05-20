namespace ArtworkService.Domain
{
    public class ArtworkImage
    {
        private int _id;
        private int _artworkId;
        private string _imageUrl;

        public ArtworkImage()
        {
            this._id = 0;
            this._artworkId = 0;
            this._imageUrl = "";
        }

        public ArtworkImage(int id, int artworkId, string? imageUrl)
        {
            this._id = id;
            this._artworkId = artworkId;
            this._imageUrl = imageUrl != null ? imageUrl : "";
        }

        public ArtworkImage(ArtworkImage image)
        {
            this._id = image._id;
            this._artworkId = image._artworkId;
            this._imageUrl = image._imageUrl;
        }

        public int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        public int ArtworkId
        {
            get { return this._artworkId; }
            set { this._artworkId = value; }
        }

        public string ImageUrl
        {
            get { return this._imageUrl; }
            set { this._imageUrl = value; }
        }
    }
}