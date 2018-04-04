using System.Threading.Tasks;

namespace com.abnamro.biz.PasswordHashing
{
    internal interface IPasswordChecker
    {
        bool CheckPassword(string plainTextPassword, string hashedPassword);
        Task<bool> CheckPasswordAsync(string plainTextPassword, string hashedPassword);
    }
}
