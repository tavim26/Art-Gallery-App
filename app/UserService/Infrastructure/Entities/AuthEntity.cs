using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UserService.Domain;

namespace UserService.Infrastructure.Entities
{
    [Table("AUTH", Schema = "dbo")]
    public class AuthEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }

        [Column("UserId")]
        [Required]
        public int UserId { get; set; }

        [Column("Email")]
        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Column("PasswordHash")]
        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; }

        public AuthEntity()
        {
            Id = 0;
            UserId = 0;
            Email = "";
            PasswordHash = "";
        }

        public AuthEntity(Auth auth)
        {
            Id = auth.Id;
            UserId = auth.UserId;
            Email = auth.Email;
            PasswordHash = auth.PasswordHash;
        }

        public Auth ToAuth()
        {
            return new Auth(Id, UserId, Email, PasswordHash);
        }
    }
}
