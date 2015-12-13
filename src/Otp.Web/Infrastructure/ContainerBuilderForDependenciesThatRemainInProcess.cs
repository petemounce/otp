using System.Reflection;
using Autofac;
using Autofac.Integration.WebApi;
using Otp.Web.OneTimePasswords;

namespace Otp.Web.Infrastructure
{
    public abstract class ContainerBuilderForDependenciesThatRemainInProcess : ContainerBuilder
    {
        protected ContainerBuilderForDependenciesThatRemainInProcess()
        {
            this.RegisterApiControllers(Assembly.GetExecutingAssembly());
            this.RegisterType<InMemoryStorage>().As<IStoreUsers>().SingleInstance();
            this.RegisterModule<AutofacFluentValidationModule>();
            this.RegisterModule<DiagnosticsModule>();
        }
    }
}