using System.Threading.Tasks;

namespace com.abnamro.biz
{
    internal interface IUserHashedPasswordSelector
    {
        UserHashedPassword SelectHashedPassword(string userName);
        Task<UserHashedPassword> SelectHashedPasswordAsync(string userName);
    }
}
