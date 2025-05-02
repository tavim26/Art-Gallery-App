using ArtworkService.Domain;
using ArtworkService.Domain.Contracts;

namespace ArtworkService.Services
{
    public class ArtworksService
    {
        private readonly IArtworkDAO _artworkDAO;

        public ArtworksService(IArtworkDAO artworkDAO)
        {
            _artworkDAO = artworkDAO;
        }

        public List<Artwork> GetArtworks()
        {
            return _artworkDAO.ListArtworks();
        }

        public Artwork? GetArtwork(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            return _artworkDAO.GetArtworkById(id);
        }

        public bool InsertArtwork(Artwork artwork)
        {
            if (artwork == null)
            {
                return false;
            }

            return _artworkDAO.InsertArtwork(artwork);
        }

        public bool UpdateArtwork(Artwork artwork)
        {
            if (artwork == null)
            {
                return false;
            }

            return _artworkDAO.UpdateArtwork(artwork);
        }

        public bool DeleteArtwork(int id)
        {
            if (id <= 0)
            {
                return false;
            }

            return _artworkDAO.DeleteArtwork(id);
        }

        public List<Artwork> SearchByTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return new List<Artwork>();
            }

            return _artworkDAO.SearchByTitle(title);
        }

        public List<Artwork> FilterByType(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                return new List<Artwork>();
            }

            return _artworkDAO.FilterByType(type);
        }

        public List<Artwork> FilterByArtistId(int artistId)
        {
            if (artistId <= 0)
            {
                return new List<Artwork>();
            }

            return _artworkDAO.FilterByArtistId(artistId);
        }

        public List<Artwork> FilterByMaxPrice(double maxPrice)
        {
            if (maxPrice < 0)
            {
                return new List<Artwork>();
            }

            return _artworkDAO.FilterByMaxPrice(maxPrice);
        }

        public List<ArtworkImage> GetArtworkImages(int artworkId)
        {
            if (artworkId <= 0)
            {
                return new List<ArtworkImage>();
            }

            return _artworkDAO.GetArtworkImages(artworkId);
        }
    }

}