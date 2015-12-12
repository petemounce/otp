using System;
using System.Net;
using Otp.Web.OneTimePasswords;
using Shouldly;
using Xunit;

namespace Otp.Tests.VerifyingPasswords
{
    public class ButPasswordDoesNotExist : AndUserExists
    {
        protected override string GivenPasswordOnRequest(OneTimePassword otp)
        {
            return Guid.NewGuid().ToString();
        }

        [Fact]
        public async void VerifyPasswordThatDoesNotExistForUserShouldGive400()
        {
            Response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }
    }
}