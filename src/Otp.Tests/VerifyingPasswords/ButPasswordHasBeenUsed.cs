using System;
using System.Net;
using System.Threading.Tasks;
using Moq;
using Otp.Web.OneTimePasswords;
using Shouldly;
using Xunit;

namespace Otp.Tests.VerifyingPasswords
{
    public class ButPasswordHasBeenUsed : AndUserExists
    {
        private const int EnoughThatTestWontTimeout = 30;

        protected override IConfigDataForOneTimePasswords GivenConfig()
        {
            var config = new Mock<IConfigDataForOneTimePasswords>();
            config.SetupGet(x => x.AllowedAgeForPasswords)
                .Returns(TimeSpan.FromSeconds(EnoughThatTestWontTimeout));
            return config.Object;
        }

        protected override async Task When()
        {
            await base.When();
            await base.When();
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
            ((int)body["Code"]).ShouldBe(45000);
        }
    }
}