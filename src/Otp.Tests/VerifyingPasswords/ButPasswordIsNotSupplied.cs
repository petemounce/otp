using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Shouldly;
using Xunit;

namespace Otp.Tests.VerifyingPasswords
{
    public class ButPasswordIsNotSupplied : AndUserExists
    {
        protected override string GivenPasswordOnRequest(JObject otp)
        {
            return null;
        }

        [Fact]
        public void ShouldGet400BadRequest()
        {
            Response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ShouldGetStandardErrorResponseBody()
        {
            var body = await DtoFrom(Response);
            ((int)body["Code"]).ShouldBe(40000);
        }
    }
}