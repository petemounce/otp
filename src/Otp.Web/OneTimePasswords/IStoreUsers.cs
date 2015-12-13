using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Otp.Web.OneTimePasswords
{
    public interface IStoreUsers
    {
        Task<ICollection<OneTimePassword>> TokensByUserIdAsync(string userId);
        Task<OneTimePassword> NewPasswordForAsync(string userId, TimeSpan allowedAge = default(TimeSpan));
        Task ConsumeTokenAsync(string userId, string password);
    }
}