using GalleryFrontend.Models;

public class ArtworkFilterModel
{
    public List<ArtworkModel> Artworks { get; set; } = new();
    public List<ArtistModel> Artists { get; set; } = new();
}
