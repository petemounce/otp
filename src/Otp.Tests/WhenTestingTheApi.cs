using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Testing;
using Moq;
using Newtonsoft.Json.Linq;
using NLog;
using NLog.Config;
using NLog.Targets;
using Otp.Web;
using Otp.Web.OneTimePasswords;

namespace Otp.Tests
{
    public abstract class WhenTestingTheApi : IDisposable
    {
        protected const int AllowedAgeInMillisecondsAccountingForExecutionTimeJitter = 32000;
        protected readonly TestServer Server;

        protected WhenTestingTheApi()
        {
            var builder = new TestsAppropriateContainerBuilder(GivenConfig());
            ConfigureLogging();
            Server = TestServer.Create(app => new Startup(builder).Configuration(app));
            Given().Wait();
            When().Wait();
        }

        private static void ConfigureLogging()
        {
            var configuration = new LoggingConfiguration();
            var mt = new MemoryTarget { Name = "memory", Layout = "${level} ${message} ${exception}" };
            var mr = new LoggingRule("*", LogLevel.Trace, mt);
            configuration.AddTarget(mt);
            configuration.LoggingRules.Add(mr);
            var ct = new ConsoleTarget { Name = "console", Layout = "${level} ${message} ${exception}" };
            var cr = new LoggingRule("*", LogLevel.Trace, ct);
            configuration.AddTarget(ct);
            configuration.LoggingRules.Add(cr);
            LogManager.Configuration = configuration;
        }

        protected virtual IConfigDataForOneTimePasswords GivenConfig()
        {
            var config = new Mock<IConfigDataForOneTimePasswords>();
            config.SetupGet(x => x.AllowedAgeForPasswords).Returns(TimeSpan.FromMilliseconds(1));
            return config.Object;
        }

        protected abstract Task Given();
        protected abstract Task When();

        public void Dispose()
        {
            Server?.Dispose();
        }

        protected async Task<JObject> DtoFrom(HttpResponseMessage res)
        {
            return JObject.Parse(await RawBody(res));
        }

        protected Task<string> RawBody(HttpResponseMessage res)
        {
            return res.Content.ReadAsStringAsync();
        }

        protected async Task<HttpResponseMessage> CreatePasswordFor(string userId)
        {
            return await Server.CreateRequest($"/api/otp/{userId}").PostAsync();
        }

        protected async Task<HttpResponseMessage> AttemptPasswordVerification(string userId, string content)
        {
            return await Server.CreateRequest($"/api/otp/{userId}/passwords")
                .And(message => message.Content = new StringContent(content, Encoding.UTF8, "application/json"))
                .SendAsync("PUT");
        }
    }
}