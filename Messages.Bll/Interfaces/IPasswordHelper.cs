
namespace Messages.Bll.Interfaces
{
    public interface IPasswordHelper
    {
        string HashPasword(string password, out byte[] salt);

        bool VerifyPassword(string password, string hash, byte[] salt);
    }
}
