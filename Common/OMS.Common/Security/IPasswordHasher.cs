namespace OMS.Common.Security
{
    public interface IPasswordHasher
    {
        (string hashedPassword, string salt) HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword, string salt);
    }
}
