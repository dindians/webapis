using com.abnamro.agents;
using System.Threading.Tasks;

namespace com.abnamro.clientapp.webapiclient.Clients
{
    internal class EmailaddressValidationWebapiClient : WebapiClient, IEmailaddressValidation
    {
        internal EmailaddressValidationWebapiClient(IWebapiContext webapiContext): base(webapiContext) { }

        bool IEmailaddressValidation.IsEmailaddressValid(Emailaddress emailaddress) => Post<Emailaddress, bool>(emailaddress);

        async Task<bool> IEmailaddressValidation.IsEmailaddressValidAsync(Emailaddress emailaddress) => await PostAsync<Emailaddress, bool>(emailaddress);
    }
}
