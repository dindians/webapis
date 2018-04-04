using com.abnamro.agents;
using System.Threading.Tasks;

namespace com.abnamro.clientapp.webapiclient.Clients
{
    public interface IEmailaddressValidation
    {
        bool IsEmailaddressValid(Emailaddress emailaddress);
        Task<bool> IsEmailaddressValidAsync(Emailaddress emailaddress);
    }
}
