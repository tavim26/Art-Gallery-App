namespace GalleryFrontend.Models.ViewModels
{

    public class ArtworkFilterModel
    {
        public List<ArtworkModel> Artworks { get; set; } = new();
        public List<ArtistModel> Artists { get; set; } = new();
    }

}