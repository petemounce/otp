using Autofac;
using Otp.Web.OneTimePasswords;

namespace Otp.Web
{
    public class ProductionAppropriateContainerBuilder : ContainerBuilderForDependenciesThatRemainInProcess
    {
        public ProductionAppropriateContainerBuilder()
        {
            this.RegisterType<Config>().As<IConfig>();
        }
    }
}