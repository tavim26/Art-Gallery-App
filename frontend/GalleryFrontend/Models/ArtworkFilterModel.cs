namespace GalleryFrontend.Models
{
    public class ArtworkFilterModel
    {
        public string SearchTitle { get; set; }
        public string SelectedType { get; set; }
        public int? SelectedArtistId { get; set; }
        public double? MaxPrice { get; set; }
        public List<ArtistModel> Artists { get; set; }
        public List<ArtworkModel> Artworks { get; set; }
    }
}