using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Autofac;
using Autofac.Integration.WebApi;
using JE.ApiValidation.WebApi;
using JE.ApiValidation.WebApi.FluentValidation;

namespace Otp.Web.Infrastructure
{
    public class MyHttpConfiguration : HttpConfiguration
    {
        public MyHttpConfiguration(ILifetimeScope container)
        {
            ConfigureExceptionLogging(container);
            ConfigureRouting();
            ConfigureRequestValidation(container);
            ConfigureResponseProcessingErrorHandling();
            DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private void ConfigureExceptionLogging(IComponentContext container)
        {
            var exceptionLogger = container.Resolve<IExceptionLogger>();
            Services.Replace(typeof (IExceptionLogger), exceptionLogger);
        }

        private void ConfigureRouting()
        {
            this.MapHttpAttributeRoutes();
        }

        private void ConfigureResponseProcessingErrorHandling()
        {
            Filters.Add(new ResponseProcessingErrorAttribute());
        }

        private void ConfigureRequestValidation(IComponentContext container)
        {
            Filters.Add(new FilterForInvalidRequestsAttribute());
        }
    }
}