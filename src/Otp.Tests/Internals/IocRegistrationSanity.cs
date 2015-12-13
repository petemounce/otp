using System;
using Autofac;
using Moq;
using Otp.Web.OneTimePasswords;
using Shouldly;
using Xunit;

namespace Otp.Tests.Internals
{
    public class IocRegistrationSanity : IDisposable
    {
        private readonly IContainer _container;

        public IocRegistrationSanity()
        {
            _container = new TestsAppropriateContainerBuilder(new Mock<IConfigDataForOneTimePasswords>().Object).Build();
        }

        public void Dispose()
        {
            _container?.Dispose();
        }

        [Fact]
        public void ShouldResolveController()
        {
            try
            {
                _container.Resolve<OtpController>();
            }
            catch (Exception ex)
            {
                ex.ShouldBeNull("Check the test console logs for the exception");
            }
        }
    }
}