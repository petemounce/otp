﻿using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Otp.Web.OneTimePasswords
{
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
        public string Password { get; private set; }
        public DateTime ExpiresAt { get; private set; }
        public ICollection<DateTime> UsageAttempts { get; private set; }
    }
}