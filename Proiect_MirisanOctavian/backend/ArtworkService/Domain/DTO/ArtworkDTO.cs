namespace ArtworkService.Domain.DTO
{
    public class ArtworkDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public int YearCreated { get; set; }
        public string Type { get; set; } = "";
        public int ArtistId { get; set; }
        public double Price { get; set; }
    }
}