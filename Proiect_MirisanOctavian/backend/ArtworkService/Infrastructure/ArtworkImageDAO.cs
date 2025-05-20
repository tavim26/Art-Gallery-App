using ArtworkService.Domain;
using ArtworkService.Domain.Contracts;
using ArtworkService.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArtworkService.Infrastructure
{
    public class ArtworkImageDAO : DbContext, IArtworkImageDAO
    {
        private DbSet<ArtworkImageEntity> ArtworkImages => Set<ArtworkImageEntity>();

        public ArtworkImageDAO(DbContextOptions<ArtworkImageDAO> options)
            : base(options) { }

        public bool InsertArtworkImage(ArtworkImage image)
        {
            if (image == null || string.IsNullOrWhiteSpace(image.ImageUrl) || image.ArtworkId <= 0)
            {
                return false;
            }

            try
            {
                ArtworkImages.Add(new ArtworkImageEntity(image));
                return SaveChanges() > 0;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"[InsertArtwork] Error inserting artwork images: {ex.Message}");
                return false;
            }
        }

        public List<ArtworkImage> GetArtworkImages(int artworkId)
        {
            if (artworkId <= 0)
            {
                return new List<ArtworkImage>();
            }

            try
            {
                return ArtworkImages
                    .Where(img => img.ArtworkId == artworkId)
                    .Select(img => img.ToArtworkImage())
                    .ToList();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"[InsertArtwork] Error getting artwork images: {ex.Message}");
                return new List<ArtworkImage>();
            }
        }
    }
}