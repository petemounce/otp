using System;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
            var response = await CreatePasswordFor("pete");
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            var parsed = await DtoFrom<OneTimePassword>(response);
            parsed.Password.ShouldSatisfyAllConditions(() => Guid.Parse(parsed.Password));
            parsed.ExpiresAt.ShouldBeInRange(now, now.AddMilliseconds(AllowedAgeInMillisecondsAccountingForExecutionTimeJitter));
        }

        [Fact]
        public async void VerifyPasswordForUserThatDoesNotExistShouldGive404()
        {
            var content = JsonConvert.SerializeObject(new {Password = "wibble"});
            var res = await _server.CreateRequest("/api/otp/some-random-clown/passwords")
                .And(message => message.Content = new StringContent(content, Encoding.UTF8, "application/json"))
                .SendAsync("PUT").ConfigureAwait(false);
            res.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async void VerifyPasswordThatDoesNotExistForUserShouldGive400()
        {
            const string userId = "pete";
            await CreatePasswordFor(userId);
            var res = await _server.CreateRequest($"/api/otp/{userId}/passwords")
                .And(message => message.Content = new StringContent("nonsense", Encoding.UTF8, "application/json"))
                .SendAsync("PUT").ConfigureAwait(false);
            res.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        private static async Task<TDto> DtoFrom<TDto>(HttpResponseMessage res)
        {
            var body = await res.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TDto>(body);
        }

        private ConfiguredTaskAwaitable<HttpResponseMessage> CreatePasswordFor(string userId)
        {
            return _server.CreateRequest($"/api/otp/{userId}").PostAsync().ConfigureAwait(false);
        }
    }
}