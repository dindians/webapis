using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace com.abnamro.webapiclient.console
{
    internal static class UntrustedCertificate
    {
        internal static bool VerifyServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            try
            {
                X509Certificate2 ca = GetDevCertificate();

                X509Chain chain2 = new X509Chain();
                chain2.ChainPolicy.ExtraStore.Add(ca);

                // Check all properties (NoFlag is correct)
                chain2.ChainPolicy.VerificationFlags = X509VerificationFlags.NoFlag;

                // This setup does not have revocation information
                chain2.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;

                // Build the chain
                chain2.Build(new X509Certificate2(certificate));

                // Are there any failures from building the chain?
                if (chain2.ChainStatus.Length == 0) return true;

                // If there is a status, verify the status is NoError
                return chain2.ChainStatus[0].Status == X509ChainStatusFlags.NoError;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return false;
        }

        internal static X509Certificate2 GetDevCertificate()
        {
            const string CA_FILE = @"C:\Users\nlacf00735\Desktop\Tests\SSL-certificates\movwdbacfdta01.acf.local.cer";
            return new X509Certificate2(CA_FILE);
        }
    }
}
