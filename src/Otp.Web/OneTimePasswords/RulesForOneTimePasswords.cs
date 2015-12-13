using System;
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
            RuleFor(x => x.HasBeenUsed)
                .Must(x => x == false)
                .When(x => x != null);
        }
    }
}