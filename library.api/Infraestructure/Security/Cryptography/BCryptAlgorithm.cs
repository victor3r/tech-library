
namespace library.api.Infraestructure.Security.Cryptography
{
    public class BCryptAlgorithm
    {
        public string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);
        public bool Verify(string password, string hashedPassword) => BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}
