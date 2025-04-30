namespace GalleryFrontend.Models
{
    public class ArtworkModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int YearCreated { get; set; }
        public string Type { get; set; }
        public int ArtistId { get; set; }
        public double Price { get; set; }

        public string ArtistName { get; set; } 
    }

}