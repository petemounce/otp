using System;
using System.Net;
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
    }
}