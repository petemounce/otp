using System.Net;
using Shouldly;
using Xunit;

namespace Otp.Tests.VerifyingPasswords
{
    public class ButPasswordHasExpired : AndUserExists
    {
        [Fact]
        public void ShouldGet400BadRequest()
        {
            Response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }
    }
}