using System;

namespace Otp.Web.OneTimePasswords
{
    public interface IConfig
    {
        TimeSpan AllowedAgeForPasswords { get; }
    }
}