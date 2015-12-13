using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Shouldly;
using Xunit;

namespace Otp.Tests.GeneratingPasswords
{
    public class WhenGeneratingANewPasswordForGivenUser : WhenTestingTheApi
    {
        private DateTime _now;
        private HttpResponseMessage _response;
        private JObject _result;

        protected override Task Given()
        {
            return Task.Run(() => { _now = DateTime.UtcNow; });
        }

        protected override async Task When()
        {
            _response = await CreatePasswordFor("pete");
            _result = await DtoFrom(_response);
        }

        [Fact]
        public void ShouldGet200Ok()
        {
            _response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Fact]
        public void ShouldHaveValidGuidAsPassword()
        {
            _result["Password"].ShouldSatisfyAllConditions(() => Guid.Parse((string)_result["Password"]));
        }

        [Fact]
        public void ShouldHaveCorrectExpiry()
        {
            var expiresAt = (DateTime)_result["ExpiresAt"];
            expiresAt.ShouldBeInRange(_now,
                _now.AddMilliseconds(AllowedAgeInMillisecondsAccountingForExecutionTimeJitter));
        }
    }
}