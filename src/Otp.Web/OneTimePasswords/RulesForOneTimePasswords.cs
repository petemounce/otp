using System;
using System.Linq;
using FluentValidation;

namespace Otp.Web.OneTimePasswords
{
    public class RulesForOneTimePasswords : AbstractValidator<OneTimePassword>
    {
        public RulesForOneTimePasswords()
        {
            RuleFor(x => x)
                .NotNull()
                .WithName("otp");
            RuleFor(x => x.ExpiresAt)
                .GreaterThanOrEqualTo(DateTime.UtcNow)
                .When(x => x != null);
            RuleFor(x => x.UsageAttempts)
                .Must(x => !x.Any())
                .When(x => x != null);
        }
    }
}