namespace GalleryFrontend.Models
{
    public class AuthModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}