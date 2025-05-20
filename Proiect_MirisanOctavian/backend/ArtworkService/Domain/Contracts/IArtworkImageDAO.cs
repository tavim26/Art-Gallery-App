namespace ArtworkService.Domain.Contracts
{
    public interface IArtworkImageDAO
    {
        bool InsertArtworkImage(ArtworkImage image);
        List<ArtworkImage> GetArtworkImages(int artworkId);
    }
}
