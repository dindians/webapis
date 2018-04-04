using System;
using System.Threading.Tasks;

namespace com.abnamro.agents
{
    public interface IMailClient: IDisposable
    {
        Task SendMailAsync(string fromMailAddress, string recipientEmailaddress, string emailSubject, string emailBody);
    }
}
