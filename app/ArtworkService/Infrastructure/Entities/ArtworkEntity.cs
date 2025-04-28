using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ArtworkService.Domain;

namespace ArtworkService.Infrastructure.Entities
{
    [Table("ARTWORK", Schema = "dbo")]
    public class ArtworkEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Title")]
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Column("YearCreated")]
        [Required]
        public int YearCreated { get; set; }

        [Column("Type")]
        [Required]
        [StringLength(50)]
        public string Type { get; set; }

        [Column("ArtistId")]
        [Required]
        public int ArtistId { get; set; }

        [Column("Price")]
        [Required]
        public double Price { get; set; } // <- adăugat

        public ArtworkEntity()
        {
            Id = 0;
            Title = "";
            YearCreated = 0;
            Type = "";
            ArtistId = 0;
            Price = 0.0;
        }

        public ArtworkEntity(Artwork artwork)
        {
            Id = artwork.Id.ArtworkId;
            Title = artwork.Title;
            YearCreated = artwork.YearCreated;
            Type = artwork.Type;
            ArtistId = artwork.ArtistId;
            Price = artwork.Price;
        }

        public Artwork ToArtwork()
        {
            return new Artwork(Id, Title, YearCreated, Type, ArtistId, Price);
        }
    }
}