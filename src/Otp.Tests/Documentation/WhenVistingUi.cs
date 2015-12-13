using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Otp.Tests.Documentation
{
    public class WhenVistingUi : WhenTestingDocs
    {
        private string _raw;

        protected override async Task When()
        {
            Response = await Server.HttpClient.GetAsync("/swagger/ui/index").ConfigureAwait(false);
            _raw = await RawBody(Response);
        }

        [Fact]
        public void ShouldGetSwaggerUi()
        {
            _raw.ShouldContain("Swagger UI");
        }
    }
}