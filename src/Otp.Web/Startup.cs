using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Otp.Web.OneTimePasswords;
using Owin;

namespace Otp.Web
{
    public class Startup
    {
        public Startup(HttpConfiguration config)
        {
            Config = config ?? new MyHttpConfiguration();
        }

        public HttpConfiguration Config { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            var container = MakeContainer();
            Config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(Config);
            app.UseWebApi(Config);
        }

        private static IContainer MakeContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<InMemoryStorage>().As<IStoreUsers>().SingleInstance();
            var container = builder.Build();
            return container;
        }
    }

    public class MyHttpConfiguration : HttpConfiguration
    {
        public MyHttpConfiguration()
        {
            ConfigureRouting();
        }

        private void ConfigureRouting()
        {
            this.MapHttpAttributeRoutes();
        }
    }
}