using System.Net;
using Shouldly;
using Xunit;

namespace Otp.Tests.VerifyingPasswords
{

    public class AndPasswordHasNotBeenUsed : AndUserExists
    {
        [Fact]
        public void ShouldGet200Ok()
        {
            Response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }
    }
}