using System.Diagnostics;

namespace Otp.Web.OneTimePasswords
{
    [DebuggerDisplay("{Password}")]
    public class OneTimePasswordVerificationRequest
    {
        public string Password { get; set; }
    }
}