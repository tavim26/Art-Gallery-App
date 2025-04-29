using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SaleService.Domain;

namespace SaleService.Infrastructure.Entities
{
    [Table("SALE", Schema = "dbo")]
    public class SaleEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }

        [Column("ArtworkId")]
        [Required]
        public int ArtworkId { get; set; }

        [Column("EmployeeId")]
        [Required]
        public int EmployeeId { get; set; }

        [Column("SaleDate")]
        [Required]
        public DateTime SaleDate { get; set; }

        [Column("Price")]
        [Required]
        public double Price { get; set; }

        public SaleEntity()
        {
            Id = 0;
            ArtworkId = 0;
            EmployeeId = 0;
            SaleDate = DateTime.MinValue;
            Price = 0.0;
        }

        public SaleEntity(Sale sale)
        {
            Id = sale.Id.Id;
            ArtworkId = sale.ArtworkId;
            EmployeeId = sale.EmployeeId;
            SaleDate = sale.SaleDate;
            Price = sale.Price;
        }

        public Sale ToSale()
        {
            return new Sale(Id, ArtworkId, EmployeeId, SaleDate, Price);
        }
    }
}