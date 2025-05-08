using ArtistService.Domain;
using ArtistService.Domain.Contracts;

namespace ArtistService.Services
{
    public class ArtistsService
    {
        private readonly IArtistDAO _artistDAO;

        public ArtistsService(IArtistDAO artistDAO)
        {
            _artistDAO = artistDAO;
        }

        public List<Artist> GetArtists() => _artistDAO.GetArtists();

        public Artist? GetArtist(int id)
        {
            if (id <= 0) 
                return null;
            return _artistDAO.GetArtistById(id);
        }

        public bool InsertArtist(Artist artist)
        {
            if (artist == null) 
                return false;
            return _artistDAO.InsertArtist(artist);
        }

        public bool UpdateArtist(Artist artist)
        {
            if (artist == null) 
                return false;
            return _artistDAO.UpdateArtist(artist);
        }

        public bool DeleteArtist(int id)
        {
            if (id <= 0) 
                return false;
            return _artistDAO.DeleteArtist(id);
        }

        public List<Artist> SearchArtistsByName(string name)
        {
            if (string.IsNullOrEmpty(name)) 
                return new List<Artist>();
            return _artistDAO.SearchArtistsByName(name);
        }
    }
}