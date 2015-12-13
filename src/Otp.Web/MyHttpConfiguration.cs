using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;

namespace Otp.Web
{
    public class MyHttpConfiguration : HttpConfiguration
    {
        public MyHttpConfiguration(ILifetimeScope container)
        {
            ConfigureRouting();
            DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private void ConfigureRouting()
        {
            this.MapHttpAttributeRoutes();
        }
    }
}