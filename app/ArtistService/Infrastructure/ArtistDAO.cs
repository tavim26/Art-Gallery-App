using ArtistService.Domain;
using ArtistService.Domain.Contracts;
using ArtistService.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArtistService.Infrastructure
{
    public class ArtistDAO : DbContext, IArtistDAO
    {
        private DbSet<ArtistEntity> _artistsSet { get; set; }

        public ArtistDAO(DbContextOptions<ArtistDAO> options)
            : base(options) { }

        public List<Artist> GetArtists()
        {
            try
            {
                List<Artist> artists = new List<Artist>();
                if (_artistsSet != null)
                    foreach (var artistEntity in _artistsSet)
                        artists.Add(artistEntity.ToArtist());
                return artists;
            }
            catch
            {
                return new List<Artist>();
            }
        }

        public Artist? GetArtistById(int id)
        {
            try
            {
                var artistEntity = _artistsSet.FirstOrDefault(a => a.Id == id);
                return artistEntity?.ToArtist();
            }
            catch
            {
                return null;
            }
        }

        public bool InsertArtist(Artist artist)
        {
            if (artist == null)
                return false;
            try
            {
                _artistsSet.Add(new ArtistEntity(artist));
                return SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateArtist(Artist artist)
        {
            if (artist == null)
                return false;
            try
            {
                _artistsSet.Update(new ArtistEntity(artist));
                return SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteArtist(int id)
        {
            try
            {
                var artist = _artistsSet.FirstOrDefault(a => a.Id == id);
                if (artist != null)
                {
                    _artistsSet.Remove(artist);
                    return SaveChanges() > 0;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public List<Artist> SearchArtistsByName(string name)
        {
            try
            {
                List<Artist> artists = new List<Artist>();
                if (_artistsSet != null)
                {
                    var query = _artistsSet.Where(a => a.Name.Contains(name));
                    foreach (var artistEntity in query)
                        artists.Add(artistEntity.ToArtist());
                }
                return artists;
            }
            catch
            {
                return new List<Artist>();
            }
        }
    }
}
