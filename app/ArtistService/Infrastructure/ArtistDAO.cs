using ArtistService.Domain;
using ArtistService.Domain.Contracts;
using ArtistService.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArtistService.Infrastructure
{
    public class ArtistDAO : DbContext, IArtistDAO
    {
        private DbSet<ArtistEntity> Artists => Set<ArtistEntity>();

        public ArtistDAO(DbContextOptions<ArtistDAO> options) : base(options) { }


        public List<Artist> GetArtists()
        {
            try
            {
                return Artists.Select(a => a.ToArtist()).ToList();
            }
            catch(Exception ex) {
            
                Console.WriteLine($"[GetArtists] Failed to retrieve artists: {ex.Message}");

                return new List<Artist>();
            }
        }

        public Artist? GetArtistById(int id)
        {
            try
            {
                var artistEntity = Artists.FirstOrDefault(a => a.Id == id);
                return artistEntity?.ToArtist();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool InsertArtist(Artist artist)
        {
            if (artist == null) return false;

            try
            {
                Artists.Add(new ArtistEntity(artist));
                return SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[InsertArtist] Failed to insert artist: {ex.Message}");

                return false;
            }
        }

        public bool UpdateArtist(Artist artist)
        {
            if (artist == null) return false;

            try
            {
                var existing = Artists.Find(artist.Id);
                if (existing == null) return false;

                existing.Name = artist.Name;
                existing.BirthDate = artist.BirthDate;
                existing.Birthplace = artist.Birthplace;
                existing.Nationality = artist.Nationality;
                existing.Photo = artist.Photo;

                return SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UpdateArtist] Failed to update artist with ID {artist.Id}: {ex.Message}");

                return false;
            }
        }

        public bool DeleteArtist(int id)
        {
            try
            {
                var artist = Artists.FirstOrDefault(a => a.Id == id);
                if (artist == null) 
                    return false;

                Artists.Remove(artist);
                return SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DeleteArtist] Failed to delete artist with ID {id}: {ex.Message}");

                return false;
            }
        }

        public List<Artist> SearchArtistsByName(string name)
        {
            try
            {
                return Artists
                    .Where(a => a.Name.Contains(name))
                    .Select(a => a.ToArtist())
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SearchArtistsByName] Failed to search artists by name '{name}': {ex.Message}");

                return new List<Artist>();
            }
        }
    }
}
