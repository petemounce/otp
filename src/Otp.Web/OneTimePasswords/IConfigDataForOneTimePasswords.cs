using System;

namespace Otp.Web.OneTimePasswords
{
    public interface IConfigDataForOneTimePasswords
    {
        TimeSpan AllowedAgeForPasswords { get; set; }
    }
}