using System.Threading.Tasks;

namespace com.abnamro.biz
{
    public interface IRegistrationcodeSelector
    {
        string SelectRegistrationcode(RegistrationcodeSelectorInput registrationcodeSelectorInput);
        Task<string> SelectRegistrationcodeAsync(RegistrationcodeSelectorInput registrationcodeSelectorInput);
    }
}
