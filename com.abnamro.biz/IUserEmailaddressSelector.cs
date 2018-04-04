using com.abnamro.agents;
using System.Threading.Tasks;

namespace com.abnamro.biz
{
    public interface IUserEmailaddressSelector
    {
        string SelectEmailaddress(UserId userId);
        Task<string> SelectEmailaddressAsync(UserId userId);
    }
}
