namespace Otp.Tests.VerifyingPasswords
{
    public abstract class AndUserExists : WhenVerifyingAPassword
    {
        protected override string GivenExistingUserId()
        {
            return "pete";
        }

        protected override string GivenUserIdOnRequest()
        {
            return "pete";
        }
    }
}