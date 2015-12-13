using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Otp.Web.OneTimePasswords
{
    /// <summary>
    /// Resource representing a one-time password
    /// </summary>
    [DebuggerDisplay("{Password} {ExpiresAt} {UsageAttempts}")]
    public class OneTimePassword
    {
        public OneTimePassword(TimeSpan allowedAge = default(TimeSpan))
        {
            Password = Guid.NewGuid().ToString();
            SetExpiresAtWithDefaultUnlessProvided(allowedAge);
            Username =
                "NOTE: security sensitive. Don't set or expose me; full credential set should not be exposed over the wire in a single lump.";
            UsageAttempts = new List<DateTime>();
        }

        private void SetExpiresAtWithDefaultUnlessProvided(TimeSpan allowedAge)
        {
            if (allowedAge == default(TimeSpan)) allowedAge = TimeSpan.FromSeconds(30);
            ExpiresAt = DateTime.UtcNow.Add(allowedAge);
        }

        private string Username { set; get; }
        /// <summary>
        /// The password
        /// </summary>
         public string Password { get; private set; }
        /// <summary>
        /// When this password expires (UTC)
        /// </summary>
         public DateTime ExpiresAt { get; private set; }
        /// <summary>
        /// When the OTP was attempted to be used
        /// </summary>
         public ICollection<DateTime> UsageAttempts { get; private set; }
    }
}