using System;

namespace Otp.Web.OneTimePasswords
{
    internal class ConfigDataForOneTimePasswords : IConfigDataForOneTimePasswords
    {
        public ConfigDataForOneTimePasswords()
        {
            AllowedAgeForPasswords = TimeSpan.FromSeconds(30);
        }

        public TimeSpan AllowedAgeForPasswords { get; set; }
    }
}