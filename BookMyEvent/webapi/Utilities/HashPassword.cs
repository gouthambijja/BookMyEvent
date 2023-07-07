using System.Security.Cryptography;
using System.Text;

namespace BookMyEvent.WebApi.Utilities
{
    public class HashPassword
    {
        public static string GetHash(string text)
        {
            var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}
