namespace GalleryFrontend.Models
{
    public class SaleModel
    {
        public int ArtworkId { get; set; }
        public int EmployeeId { get; set; }  
        public DateTime SaleDate { get; set; }
        public double Price { get; set; }
    }
}
