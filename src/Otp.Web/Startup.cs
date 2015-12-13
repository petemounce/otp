using System.Web.Http;
using Autofac;
using Microsoft.Owin;
using Otp.Web;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace Otp.Web
{
    public class Startup
    {
        private readonly IContainer _container;

        public Startup(ContainerBuilder builder = null)
        {
            _container = CreateContainer(builder);
            Config = new MyHttpConfiguration(_container);
        }

        private static IContainer CreateContainer(ContainerBuilder builder)
        {
            return (builder ?? new ProductionAppropriateContainerBuilder()).Build();
        }

        public HttpConfiguration Config { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            app.UseAutofacMiddleware(_container);
            app.UseAutofacWebApi(Config);
            app.UseWebApi(Config);
        }
    }
}