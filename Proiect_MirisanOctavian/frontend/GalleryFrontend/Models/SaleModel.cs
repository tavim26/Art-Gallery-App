namespace GalleryFrontend.Models
{
    public class SaleModel
    {
        public int Id { get; set; }                 
        public int ArtworkId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime SaleDate { get; set; }
        public double Price { get; set; }

        public string ArtworkTitle { get; set; } = "";
        public string EmployeeName { get; set; } = "";
    }
}