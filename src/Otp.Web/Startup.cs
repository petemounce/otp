using System.Web.Http;
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
            app.UseWebApi(Config);
        }
    }

    public class MyHttpConfiguration : HttpConfiguration
    {
        public MyHttpConfiguration()
        {
            this.MapHttpAttributeRoutes();

            Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new {id = RouteParameter.Optional}
                );
        }
    }
}