using System;
using System.Net;
using Microsoft.Owin.Testing;
using Newtonsoft.Json;
using Otp.Web;
using Otp.Web.OneTimePasswords;
using Shouldly;
using Xunit;

namespace Otp.Tests
{
    public class WhenTestingAllTheThings : IDisposable
    {
        private const int AllowedAgeInMillisecondsAccountingForExecutionTimeJitter = 32000;
        private readonly TestServer _server;

        public WhenTestingAllTheThings()
        {
            _server = TestServer.Create<Startup>();
        }

        public void Dispose()
        {
            _server?.Dispose();
        }

        [Fact]
        public async void CanGenerateANewPasswordForGivenUser()
        {
            var now = DateTime.UtcNow;
            var res = await _server.CreateRequest("/api/otp/pete").PostAsync().ConfigureAwait(false);
            res.StatusCode.ShouldBe(HttpStatusCode.OK);
            var body = await res.Content.ReadAsStringAsync();
            var parsed = JsonConvert.DeserializeObject<OneTimePassword>(body);
            parsed.Password.ShouldSatisfyAllConditions(() => Guid.Parse(parsed.Password));
            parsed.ExpiresAt.ShouldBeInRange(now, now.AddMilliseconds(AllowedAgeInMillisecondsAccountingForExecutionTimeJitter));
        }

        [Fact]
        public async void CanGetOneOtp()
        {
            var res = await _server.HttpClient.GetAsync("/api/otp/1").ConfigureAwait(false);
            res.StatusCode.ShouldBe(HttpStatusCode.OK);
        }
    }
}