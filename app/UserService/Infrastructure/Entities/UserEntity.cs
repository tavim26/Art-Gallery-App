using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UserService.Domain;

namespace UserService.Infrastructure.Entities
{
    [Table("USER", Schema = "dbo")]
    public class UserEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Name")]
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Column("Role")]
        [Required]
        [StringLength(50)]
        public string Role { get; set; }

        [Column("Phone")]
        [StringLength(20)]
        public string? Phone { get; set; }

        public UserEntity()
        {
            Id = 0;
            Name = "";
            Role = "";
            Phone = "";
        }

        public UserEntity(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Role = user.Role;
            Phone = user.Phone;
        }

        public User ToUser()
        {
            return new User(Id, Name, Role, Phone);
        }
    }
}