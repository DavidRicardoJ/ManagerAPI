

namespace Manager.Service.Cryptography.Interfaces
{
    public interface IBCrypt
    {
        string GetRandomSalt();
        bool ValidatePassword(string password, string correctHash);

        string HashPassword(string password);
    }
}
