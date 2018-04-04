using System;

namespace com.abnamro.biz.PasswordHashing
{
    internal static class PasswordHasher
    {
        internal static IPasswordHasher Create(bool useBCrypt = true) => useBCrypt? BCryptPasswordHasher.CreateHasher(): ShaOnePasswordHasher.CreateHasher();
    }
}
