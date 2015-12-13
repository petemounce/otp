using System;
using System.Net;
using Moq;
using Otp.Web.OneTimePasswords;
using Shouldly;
using Xunit;

namespace Otp.Tests.VerifyingPasswords
{
    public class AndPasswordHasNotBeenUsed : AndUserExists
    {
        protected override IConfigDataForOneTimePasswords GivenConfig()
        {
            var config = new Mock<IConfigDataForOneTimePasswords>();
            config.SetupGet(x => x.AllowedAgeForPasswords)
                .Returns(TimeSpan.FromSeconds(40));
            return config.Object;
        }

        [Fact]
        public void ShouldGet200Ok()
        {
            Response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }
    }
}