using System.Security.Cryptography;
using System.Text;

namespace UserService.Utils
{
    public static class HashHelper
    {
        public static string HashPassword(string plainText)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(plainText);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}