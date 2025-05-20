namespace SaleService.Domain.DTO
{
    public class SaleDTO
    {
        public int Id { get; set; }
        public int ArtworkId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime SaleDate { get; set; }
        public double Price { get; set; }
    }
}