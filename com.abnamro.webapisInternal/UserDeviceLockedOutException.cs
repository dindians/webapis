using System;

namespace com.abnamro.webapisInternal
{
    public class UserDeviceLockedOutException: Exception
    {
        internal UserDeviceLockedOutException(string message) : base(message) { }
    }
}
