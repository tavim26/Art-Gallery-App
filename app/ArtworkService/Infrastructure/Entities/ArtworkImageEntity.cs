using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ArtworkService.Domain;

namespace ArtworkService.Infrastructure.Entities
{
    [Table("ARTWORK_IMAGE", Schema = "dbo")]
    public class ArtworkImageEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }

        [Column("ArtworkId")]
        [Required]
        public int ArtworkId { get; set; }

        [Column("ImageUrl")]
        [Required]
        public string ImageUrl { get; set; }

        public ArtworkImageEntity()
        {
            Id = 0;
            ArtworkId = 0;
            ImageUrl = "";
        }

        public ArtworkImageEntity(ArtworkImage image)
        {
            Id = image.Id;
            ArtworkId = image.ArtworkId;
            ImageUrl = image.ImageUrl;
        }

        public ArtworkImage ToArtworkImage()
        {
            return new ArtworkImage(Id, ArtworkId, ImageUrl);
        }
    }
}