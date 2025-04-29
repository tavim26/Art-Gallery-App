namespace GalleryFrontend.Models
{
    public class ArtistModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Birthplace { get; set; }
        public string Nationality { get; set; }
        public string Photo { get; set; }
    }
}