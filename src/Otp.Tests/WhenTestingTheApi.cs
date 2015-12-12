using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Testing;
using Newtonsoft.Json;
using Otp.Web;

namespace Otp.Tests
{
    public abstract class WhenTestingTheApi : IDisposable
    {
        protected const int AllowedAgeInMillisecondsAccountingForExecutionTimeJitter = 32000;
        private readonly TestServer _server;

        protected WhenTestingTheApi()
        {
            _server = TestServer.Create<Startup>();
            Given().Wait();
            When().Wait();
        }

        protected abstract Task Given();
        protected abstract Task When();

        public void Dispose()
        {
            _server?.Dispose();
        }

        protected async Task<TDto> DtoFrom<TDto>(HttpResponseMessage res)
        {
            var body = await res.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TDto>(body);
        }

        protected ConfiguredTaskAwaitable<HttpResponseMessage> CreatePasswordFor(string userId)
        {
            return _server.CreateRequest($"/api/otp/{userId}").PostAsync().ConfigureAwait(false);
        }

        protected ConfiguredTaskAwaitable<HttpResponseMessage> AttemptPasswordVerification(string userId, string content)
        {
            return _server.CreateRequest($"/api/otp/{userId}/passwords")
                .And(message => message.Content = new StringContent(content, Encoding.UTF8, "application/json"))
                .SendAsync("PUT").ConfigureAwait(false);
        }
    }
}