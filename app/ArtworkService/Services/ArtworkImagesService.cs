using ArtworkService.Domain;
using ArtworkService.Domain.Contracts;

namespace ArtworkService.Services
{
    public class ArtworkImagesService
    {
        private IArtworkImageDAO _artworkImageDAO;

        public ArtworkImagesService(IArtworkImageDAO artworkImageDAO)
        {
            this._artworkImageDAO = artworkImageDAO;
        }

        public bool InsertArtworkImage(ArtworkImage image)
        {
            if (image == null)
                return false;
            return _artworkImageDAO.InsertArtworkImage(image);
        }

        public List<ArtworkImage> GetArtworkImages(int artworkId)
        {
            if (artworkId <= 0)
                return new List<ArtworkImage>();
            return _artworkImageDAO.GetArtworkImages(artworkId);
        }
    }
}
