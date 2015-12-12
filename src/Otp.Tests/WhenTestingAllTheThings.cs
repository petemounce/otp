using System.Net;
using Microsoft.Owin.Testing;
using Otp.Web;
using Shouldly;
using Xunit;

namespace Otp.Tests
{
    public class WhenTestingAllTheThings
    {
        [Fact]
        public async void CanGetOtpList()
        {
            using (var server = TestServer.Create<Startup>())
            {
                var res = await server.HttpClient.GetAsync("/api/Otp").ConfigureAwait(false);
                res.StatusCode.ShouldBe(HttpStatusCode.OK);
            }
        }
    }
}