using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Otp.Web.OneTimePasswords
{
    public class InMemoryStorage : IStoreUsers
    {
        private readonly IDictionary<string, ICollection<OneTimePassword>> _data;

        public InMemoryStorage()
        {
            _data = new ConcurrentDictionary<string, ICollection<OneTimePassword>>(StringComparer.InvariantCulture);
        }

        public Task<ICollection<OneTimePassword>> TokensByUserIdAsync(string userId)
        {
            return Task.Run(() => !_data.ContainsKey(userId) ? new List<OneTimePassword>() : _data[userId]);
        }

        public Task<OneTimePassword> NewPasswordForAsync(string userId, TimeSpan allowedAge = default(TimeSpan))
        {
            return Task.Run(() =>
            {
                if (!_data.ContainsKey(userId))
                {
                    _data[userId] = new List<OneTimePassword>();
                }
                var otp = new OneTimePassword(allowedAge);
                _data[userId].Add(otp);
                return otp;
            });
        }

        public async Task ConsumeTokenAsync(string userId, string password)
        {
            await Task.Run(() =>
            {
                _data[userId].Single(otp => otp.Password.Equals(password)).UsageAttempts.Add(DateTime.UtcNow);
            });
        }
    }
}