namespace ArtistService.Domain.DTO
{
    public class ArtistDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string? BirthDate { get; set; } 
        public string Birthplace { get; set; } = "";
        public string Nationality { get; set; } = "";
        public string Photo { get; set; } = "";
    }
}