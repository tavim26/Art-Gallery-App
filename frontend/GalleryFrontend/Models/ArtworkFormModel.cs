using GalleryFrontend.Models;


namespace GalleryFrontend.Models
{
    public class ArtworkFormModel
    {
        public ArtworkModel Artwork { get; set; }
        public List<ArtistModel> Artists { get; set; }
    }

}