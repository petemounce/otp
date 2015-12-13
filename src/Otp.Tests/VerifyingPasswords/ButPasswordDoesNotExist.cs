using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Shouldly;
using Xunit;

namespace Otp.Tests.VerifyingPasswords
{
    public class ButPasswordDoesNotExist : AndUserExists
    {
        protected override string GivenPasswordOnRequest(JObject otp)
        {
            return Guid.NewGuid().ToString();
        }

        [Fact]
        public void VerifyPasswordThatDoesNotExistForUserShouldGive400() => Response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        [Fact]
        public async Task ShouldGetStandardErrorResponseBody()
        {
            var body = await DtoFrom(Response);
            ((int)body["Code"]).ShouldBe(45000);
        }
    }
}