namespace ArtworkService.Domain
{
    public class Artwork
    {
        private ArtworkID _id;
        private string _title;
        private int _yearCreated;
        private string _type;
        private int _artistId;

        private double _price;

        public Artwork()
        {
            this._id = new ArtworkID();
            this._title = "";
            this._yearCreated = 0;
            this._type = "";
            this._artistId = 0;
            this._price = 0.0;
        }

        public Artwork(ArtworkID id, string? title, int yearCreated, string? type, int artistId,double price)
        {
            this._id = id;
            this._title = title != null ? title : "";
            this._yearCreated = yearCreated;
            this._type = type != null ? type : "";
            this._artistId = artistId;
            this._price = price;
        }

        public Artwork(int artworkId, string? title, int yearCreated, string? type, int artistId,double price)
        {
            this._id = new ArtworkID(artworkId);
            this._title = title != null ? title : "";
            this._yearCreated = yearCreated;
            this._type = type != null ? type : "";
            this._artistId = artistId;
            this._price = price;
        }

        public Artwork(Artwork artwork)
        {
            this._id = new ArtworkID(artwork._id);
            this._title = artwork._title;
            this._yearCreated = artwork._yearCreated;
            this._type = artwork._type;
            this._artistId = artwork._artistId;
            this._price = artwork._price;
        }

        public ArtworkID Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        public string Title
        {
            get { return this._title; }
            set { this._title = value; }
        }

        public int YearCreated
        {
            get { return this._yearCreated; }
            set { this._yearCreated = value; }
        }

        public string Type
        {
            get { return this._type; }
            set { this._type = value; }
        }

        public int ArtistId
        {
            get { return this._artistId; }
            set { this._artistId = value; }
        }

        public double Price
        {
            get { return this._price; }
            set { this._price = value; }
        }
    }
}
