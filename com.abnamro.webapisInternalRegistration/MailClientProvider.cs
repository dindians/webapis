using com.abnamro.agents;
using com.abnamro.webapi.core;

namespace com.abnamro.webapisInternalRegistration
{
    internal class MailClientProvider : IMailClientProvider
    {
        private enum SmtpAppSettingsKey
        {
            SmtpHost,
            SmtpPort
        }

        IMailClient IMailClientProvider.GetMailClient()
        {
            return new SmtpMailClient(AppSettings.GetStringValue(nameof(SmtpAppSettingsKey.SmtpHost)), AppSettings.GetIntValue(nameof(SmtpAppSettingsKey.SmtpPort)));
        }
    }
}
