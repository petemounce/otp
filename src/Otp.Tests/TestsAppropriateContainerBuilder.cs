using Autofac;
using Otp.Web;
using Otp.Web.OneTimePasswords;

namespace Otp.Tests
{
    public class TestsAppropriateContainerBuilder : ContainerBuilderForDependenciesThatRemainInProcess
    {
        public TestsAppropriateContainerBuilder(IConfig config)
        {
            this.RegisterInstance(config).As<IConfig>();
        }
    }
}