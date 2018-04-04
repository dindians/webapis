namespace com.abnamro.biz.PasswordHashing
{
    internal interface IPasswordHasher
    {
        string HashPassword(string password);
    }
}
