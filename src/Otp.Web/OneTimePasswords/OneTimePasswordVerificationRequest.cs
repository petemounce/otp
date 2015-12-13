using System.Diagnostics;

namespace Otp.Web.OneTimePasswords
{
    /// <summary>
    /// The request to use a one-time password
    /// </summary>
    [DebuggerDisplay("{Password}")]
    public class OneTimePasswordVerificationRequest
    {
        /// <summary>
        /// The password to use
        /// </summary>
        public string Password { get; set; }
    }
}