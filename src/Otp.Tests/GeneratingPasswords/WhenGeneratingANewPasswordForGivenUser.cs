using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Otp.Web.OneTimePasswords;
using Shouldly;
using Xunit;

namespace Otp.Tests.GeneratingPasswords
{
    public class WhenGeneratingANewPasswordForGivenUser : WhenTestingTheApi
    {
        private DateTime _now;
        private HttpResponseMessage _response;
        private OneTimePassword _result;

        protected override Task Given()
        {
            return Task.Run(() =>
            {
                _now = DateTime.UtcNow;
            });
        }

        protected override async Task When()
        {
            _response = await CreatePasswordFor("pete");
            _result = await DtoFrom<OneTimePassword>(_response);
        }

        [Fact]
        public async Task ShouldGet200Ok()
        {
            _response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Fact]
        public async Task ShouldHaveValidGuidAsPassword()
        {
            _result.Password.ShouldSatisfyAllConditions(() => Guid.Parse(_result.Password));
        }

        [Fact]
        public async Task ShouldHaveCorrectExpiry()
        {
            _result.ExpiresAt.ShouldBeInRange(_now,
                _now.AddMilliseconds(AllowedAgeInMillisecondsAccountingForExecutionTimeJitter));
        }
    }
}