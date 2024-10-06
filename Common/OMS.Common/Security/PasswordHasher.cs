using System.Security.Cryptography;
using System.Text;

namespace OMS.Common.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        private const short SaltLength = 256;
        private const int Iterations = 100000;
        private const short HashedLength = 64;
        private const string SaltCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()-_=+[{]}\\|;:'\",<.>/?`~";

        public (string hashedPassword, string salt) HashPassword(string password)
        {
            string salt = GenerateSalt(SaltLength);
            var hashedPassword = HashWithSalt(password, salt);
            return (Convert.ToBase64String(hashedPassword), salt);
        }

        public bool VerifyPassword(string password, string hashedPassword, string salt)
        {
            var hashedInputPassword = HashWithSalt(password, salt);
            return Convert.ToBase64String(hashedInputPassword) == hashedPassword;
        }

        private byte[] HashWithSalt(string password, string salt)
        {
            var saltBytes = Encoding.UTF8.GetBytes(salt);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, Iterations))
            {
                return pbkdf2.GetBytes(HashedLength);
            }
        }

        private string GenerateSalt(int length)
        {
            var saltBytes = new byte[length];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }

            var salt = new StringBuilder(length);
            foreach (var byteValue in saltBytes)
            {
                salt.Append(SaltCharacters[byteValue % SaltCharacters.Length]);
            }

            return salt.ToString();
        }
    }
}
