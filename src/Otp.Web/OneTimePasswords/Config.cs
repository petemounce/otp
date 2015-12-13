using System;

namespace Otp.Web.OneTimePasswords
{
    internal class Config : IConfig
    {
        public Config()
        {
            AllowedAgeForPasswords = TimeSpan.FromSeconds(30);
        }
        public TimeSpan AllowedAgeForPasswords { get; set; }
    }
}