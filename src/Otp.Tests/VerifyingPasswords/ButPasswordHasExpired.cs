using System.Net;
using System.Threading.Tasks;
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

        [Fact]
        public async Task ShouldGetStandardErrorResponseBody()
        {
            var body = await DtoFrom(Response);
            ((int)body["Code"]).ShouldBe(45000);
        }
    }
}