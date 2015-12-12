using System;

namespace Otp.Web.OneTimePasswords
{
    public class OneTimePassword
    {
        public OneTimePassword()
        {
            Password = Guid.NewGuid().ToString();
            ExpiresAt = DateTime.UtcNow.AddSeconds(30);
            Username =
                "NOTE: security sensitive. Don't set or expose me; full credential set should not be exposed over the wire in a single lump.";
        }

        private string Username { set; get; }
        public string Password { get; private set; }
        public DateTime ExpiresAt { get; private set; }
    }
}