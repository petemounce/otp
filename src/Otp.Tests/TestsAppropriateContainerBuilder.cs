using Autofac;
using Otp.Web;
using Otp.Web.Infrastructure;
using Otp.Web.OneTimePasswords;

namespace Otp.Tests
{
    public class TestsAppropriateContainerBuilder : ContainerBuilderForDependenciesThatRemainInProcess
    {
        public TestsAppropriateContainerBuilder(IConfigDataForOneTimePasswords config)
        {
            this.RegisterInstance(config).As<IConfigDataForOneTimePasswords>();
        }
    }
}