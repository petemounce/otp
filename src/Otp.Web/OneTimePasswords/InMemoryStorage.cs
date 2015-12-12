using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
        public Task<bool> UserExistsAsync(string userId)
        {
            return Task.Run(() => _data.ContainsKey(userId));
        }

        public Task<OneTimePassword> NewPasswordForAsync(string userId)
        {
            return Task.Run(() =>
            {
                if (!_data.ContainsKey(userId))
                {
                    _data[userId] = new List<OneTimePassword>();
                }
                var otp = new OneTimePassword();
                _data[userId].Add(otp);
                return otp;
            });
        }
    }
}