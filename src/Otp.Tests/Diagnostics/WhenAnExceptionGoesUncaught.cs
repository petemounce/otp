using System.Collections.Generic;
using System.Threading.Tasks;
using NLog;
using NLog.Targets;
using Shouldly;
using Xunit;

namespace Otp.Tests.Diagnostics
{
    public class WhenAnExceptionGoesUncaught : WhenTestingTheApi
    {
        private string _body;
        private IList<string> _logs;

        protected override Task Given()
        {
            return Task.Run(() => { });
        }

        protected override async Task When()
        {
            var response = await Server.HttpClient.GetAsync("/api/internal/test-exception");
            _body = await RawBody(response);
            _logs = LogManager.Configuration.FindTargetByName<MemoryTarget>("memory").Logs;
        }

        [Fact]
        public void ShouldMentionException()
        {
            _body.ShouldContain("Error");
        }

        [Fact]
        public void ShouldLogException()
        {
            _logs.ShouldContain(x => x.Contains("Unhandled exception"));
        }
    }
}