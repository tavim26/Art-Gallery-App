using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ArtistService.Domain;

namespace ArtistService.Infrastructure.Entities
{
    [Table("ARTIST", Schema = "dbo")]
    public class ArtistEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Name")]
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Column("BirthDate")]
        public DateTime? BirthDate { get; set; }

        [Column("Birthplace")]
        [StringLength(100)]
        public string? Birthplace { get; set; }

        [Column("Nationality")]
        [StringLength(50)]
        public string? Nationality { get; set; }

        [Column("Photo")]
        [StringLength(255)]
        public string? Photo { get; set; }

        public ArtistEntity()
        {
            Id = 0;
            Name = "";
            BirthDate = null;
            Birthplace = "";
            Nationality = "";
            Photo = "";
        }

        public ArtistEntity(Artist artist)
        {
            Id = artist.Id.ArtistId;
            Name = artist.Name;
            BirthDate = artist.BirthDate;
            Birthplace = artist.Birthplace;
            Nationality = artist.Nationality;
            Photo = artist.Photo;
        }

        public Artist ToArtist()
        {
            return new Artist(Id, Name, BirthDate, Birthplace, Nationality, Photo);
        }
    }
}