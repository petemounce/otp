using Autofac;
using Otp.Web.OneTimePasswords;

namespace Otp.Web.Infrastructure
{
    public class ProductionAppropriateContainerBuilder : ContainerBuilderForDependenciesThatRemainInProcess
    {
        public ProductionAppropriateContainerBuilder()
        {
            this.RegisterType<ConfigDataForOneTimePasswords>().As<IConfigDataForOneTimePasswords>();
        }
    }
}