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
            {
                Console.WriteLine("DAO: null artist");
                return false;
            }

            try
            {
                Console.WriteLine($"DAO: Updating artist ID={artist.Id}");

                var existing = _artistsSet.Find(artist.Id);
                if (existing == null)
                {
                    Console.WriteLine("DAO: artist not found in DB");
                    return false;
                }

                // Copiere manuală a câmpurilor
                existing.Name = artist.Name;
                existing.BirthDate = artist.BirthDate;
                existing.Birthplace = artist.Birthplace;
                existing.Nationality = artist.Nationality;
                existing.Photo = artist.Photo;

                var result = SaveChanges();
                Console.WriteLine($"DAO: SaveChanges result = {result}");

                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("DAO exception: " + ex.Message);
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
