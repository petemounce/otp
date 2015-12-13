using System.Net;
using Shouldly;
using Xunit;

namespace Otp.Tests.VerifyingPasswords
{
    public class ButUserDoesNotExist : WhenVerifyingAPassword
    {
        protected override string GivenExistingUserId()
        {
            return "pete";
        }

        protected override string GivenUserIdOnRequest()
        {
            return "different from pete";
        }

        [Fact]
        public void ShouldGet404NotFound()
        {
            Response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}