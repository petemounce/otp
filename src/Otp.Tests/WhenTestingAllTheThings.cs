using System;
using System.Net;
using Microsoft.Owin.Testing;
using Otp.Web;
using Shouldly;
using Xunit;

namespace Otp.Tests
{
    public class WhenTestingAllTheThings : IDisposable
    {
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
        public async void CanGetOtpList()
        {
            var res = await _server.HttpClient.GetAsync("/api/otp").ConfigureAwait(false);
            res.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Fact]
        public async void CanGetOneOtp()
        {
            var res = await _server.HttpClient.GetAsync("/api/otp/1").ConfigureAwait(false);
            res.StatusCode.ShouldBe(HttpStatusCode.OK);
        }
    }
}