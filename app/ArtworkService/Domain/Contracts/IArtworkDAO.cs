namespace ArtworkService.Domain.Contracts
{
    public interface IArtworkDAO
    {
        List<Artwork> ListArtworks();
        Artwork? GetArtworkById(int id);
        List<ArtworkImage> GetArtworkImages(int artworkId);
        bool InsertArtwork(Artwork artwork);
        bool UpdateArtwork(Artwork artwork);
        bool DeleteArtwork(int id);

        List<Artwork> SearchByTitle(string title);
        List<Artwork> FilterByType(string type);
        List<Artwork> FilterByArtistId(int artistId);
        List<Artwork> FilterByMaxPrice(double maxPrice);
    }
}