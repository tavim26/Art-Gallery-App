using ArtworkService.Domain;
using ArtworkService.Domain.Contracts;

namespace ArtworkService.Services
{
    public class ArtworksService
    {
        private IArtworkDAO _artworkDAO;

        public ArtworksService(IArtworkDAO artworkDAO)
        {
            this._artworkDAO = artworkDAO;
        }

        public List<Artwork> GetArtworks()
        {
            return _artworkDAO.Artworks();
        }

        public Artwork? GetArtwork(int id)
        {
            if (id <= 0)
                return null;
            return _artworkDAO.GetArtworkById(id);
        }

        public List<ArtworkImage> GetArtworkImages(int artworkId)
        {
            if (artworkId <= 0)
                return new List<ArtworkImage>();
            return _artworkDAO.GetArtworkImages(artworkId);
        }

        public bool InsertArtwork(Artwork artwork)
        {
            if (artwork == null)
                return false;
            return _artworkDAO.InsertArtwork(artwork);
        }

        public bool UpdateArtwork(Artwork artwork)
        {
            if (artwork == null)
                return false;
            return _artworkDAO.UpdateArtwork(artwork);
        }

        public bool DeleteArtwork(int id)
        {
            if (id <= 0)
                return false;
            return _artworkDAO.DeleteArtwork(id);
        }


        public List<Artwork> SearchByTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
                return new List<Artwork>();
            return _artworkDAO.SearchByTitle(title);
        }

        public List<Artwork> FilterByType(string type)
        {
            if (string.IsNullOrEmpty(type))
                return new List<Artwork>();
            return _artworkDAO.FilterByType(type);
        }

        public List<Artwork> FilterByMaxPrice(double maxPrice)
        {
            if (maxPrice < 0)
                return new List<Artwork>();
            return _artworkDAO.FilterByMaxPrice(maxPrice);
        }
    }
}