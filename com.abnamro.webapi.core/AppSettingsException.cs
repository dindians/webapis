using System;

namespace com.abnamro.webapi.core
{
    public class AppSettingsException: Exception
    {
        internal AppSettingsException(string message) : base(message) {}
    }
}
