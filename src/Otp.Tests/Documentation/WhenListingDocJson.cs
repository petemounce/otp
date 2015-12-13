using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Otp.Tests.Documentation
{
    public class WhenListingDocJson : WhenTestingDocs
    {
        protected override async Task When()
        {
            Response = await Server.HttpClient.GetAsync("/swagger/docs/v1").ConfigureAwait(false);
            Body = await DtoFrom(Response).ConfigureAwait(false);
        }

        [Fact]
        public void ShouldHaveDocsForOtpVerify()
        {
            Body["paths"]["/api/otp/{userId}/passwords"]["put"].ShouldNotBeNull();
        }

        [Fact]
        public void ShouldHaveDocsForOtpCreate()
        {
            Body["paths"]["/api/otp/{userId}"]["post"].ShouldNotBeNull();
        }

        [Fact]
        public void ShouldHaveOtpVerifyRequestDefinition()
        {
            Body["definitions"]["OneTimePasswordVerificationRequest"].ShouldNotBeNull();
        }

        [Fact]
        public void ShouldHaveStandardErrorResponseDefinition()
        {
            Body["definitions"]["StandardErrorResponse"].ShouldNotBeNull();
            Body["definitions"]["Problem"].ShouldNotBeNull();
        }
    }
}