using System.Threading.Tasks;

namespace Otp.Web.OneTimePasswords
{
    public interface IStoreUsers
    {
        Task<bool> UserExistsAsync(string userId);
        Task<OneTimePassword> NewPasswordForAsync(string userId);
    }
}