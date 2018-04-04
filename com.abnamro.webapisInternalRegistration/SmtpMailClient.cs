using com.abnamro.agents;
using System.Net.Mail;
using System.Threading.Tasks;

namespace com.abnamro.webapisInternalRegistration
{
    // todo: replace System.Net.Mail.SmtpClient with https://github.com/jstedfast/MailKit.
    // Microsoft .NET Framework 4.7 documentation says System.Net.Mail.SmtpClient is obsoleted.
    // See also https://www.infoq.com/news/2017/04/MailKit-MimeKit-Official and https://docs.microsoft.com/en-us/dotnet/api/system.net.mail.smtpclient?view=netframework-4.7 
    internal class SmtpMailClient : SmtpClient, IMailClient
    {
        internal SmtpMailClient(string host, int port) : base(host, port) { }

        async Task IMailClient.SendMailAsync(string fromMailAddress, string recipientEmailaddress, string emailSubject, string emailBody) => await SendMailAsync(fromMailAddress, recipientEmailaddress, emailSubject, emailBody);
    }
}
