using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Testing;
using Moq;
using Newtonsoft.Json.Linq;
using Otp.Web;
using Otp.Web.OneTimePasswords;

namespace Otp.Tests
{
    public abstract class WhenTestingTheApi : IDisposable
    {
        protected const int AllowedAgeInMillisecondsAccountingForExecutionTimeJitter = 32000;
        private readonly TestServer _server;

        protected WhenTestingTheApi()
        {
            var builder = new TestsAppropriateContainerBuilder(GivenConfig());
            _server = TestServer.Create(app => new Startup(builder).Configuration(app));
            Given().Wait();
            When().Wait();
        }

        protected virtual IConfig GivenConfig()
        {
            var config = new Mock<IConfig>();
            config.SetupGet(x => x.AllowedAgeForPasswords).Returns(TimeSpan.FromMilliseconds(1));
            return config.Object;
        }

        protected abstract Task Given();
        protected abstract Task When();

        public void Dispose()
        {
            _server?.Dispose();
        }

        protected async Task<JObject> DtoFrom(HttpResponseMessage res)
        {
            var body = await res.Content.ReadAsStringAsync();
            return JObject.Parse(body);
        }

        protected async Task<HttpResponseMessage> CreatePasswordFor(string userId)
        {
            return await _server.CreateRequest($"/api/otp/{userId}").PostAsync();
        }

        protected async Task<HttpResponseMessage> AttemptPasswordVerification(string userId, string content)
        {
            return await _server.CreateRequest($"/api/otp/{userId}/passwords")
                .And(message => message.Content = new StringContent(content, Encoding.UTF8, "application/json"))
                .SendAsync("PUT");
        }
    }
}