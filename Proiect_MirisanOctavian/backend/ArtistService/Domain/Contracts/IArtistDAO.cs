namespace ArtistService.Domain.Contracts
{
    public interface IArtistDAO
    {
        List<Artist> GetArtists();
        Artist? GetArtistById(int id);
        bool InsertArtist(Artist artist);
        bool UpdateArtist(Artist artist);
        bool DeleteArtist(int id);
        List<Artist> SearchArtistsByName(string name);
    }
}