using System;
using FluentValidation;

namespace Otp.Web.OneTimePasswords
{
    public class RequestValidationFulesForPasswordVerification : AbstractValidator<OneTimePasswordVerificationRequest>
    {
        public RequestValidationFulesForPasswordVerification()
        {
            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .Must(p =>
                {
                    Guid g;
                    return Guid.TryParse(p, out g);
                });
        }
    }
}