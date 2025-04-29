using ArtworkService.Domain;
using ArtworkService.Domain.Contracts;
using ArtworkService.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArtworkService.Infrastructure
{
    public class ArtworkImageDAO : DbContext, IArtworkImageDAO
    {
        private DbSet<ArtworkImageEntity> _artworkImagesSet { get; set; }

        public ArtworkImageDAO(DbContextOptions<ArtworkImageDAO> options)
            : base(options) { }

        public bool InsertArtworkImage(ArtworkImage image)
        {
            if (image == null)
                return false;
            try
            {
                _artworkImagesSet.Add(new ArtworkImageEntity(image));
                return SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public List<ArtworkImage> GetArtworkImages(int artworkId)
        {
            try
            {
                List<ArtworkImage> images = new List<ArtworkImage>();
                if (_artworkImagesSet != null)
                {
                    var query = _artworkImagesSet.Where(img => img.ArtworkId == artworkId);
                    foreach (var imgEntity in query)
                        images.Add(imgEntity.ToArtworkImage());
                }
                return images;
            }
            catch
            {
                return new List<ArtworkImage>();
            }
        }
    }
}
