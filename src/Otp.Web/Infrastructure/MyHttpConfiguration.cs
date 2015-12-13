using System;
using System.IO;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Autofac;
using Autofac.Integration.WebApi;
using JE.ApiValidation.WebApi;
using JE.ApiValidation.WebApi.FluentValidation;
using Otp.Web.Infrastructure.Docs;
using Swashbuckle.Application;

namespace Otp.Web.Infrastructure
{
    public class MyHttpConfiguration : HttpConfiguration
    {
        public MyHttpConfiguration(ILifetimeScope container)
        {
            ConfigureExceptionLogging(container);
            ConfigureRouting();
            ConfigureRequestValidation();
            ConfigureResponseProcessingErrorHandling();
            ConfigureSwaggerDocs();
            DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private void ConfigureSwaggerDocs()
        {
            this.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "One time passwords");
                c.OperationFilter<AddDefaultErrorResponse>();
                c.Schemes(new[] { "http" });
                if (new FileInfo($@"{AppDomain.CurrentDomain.BaseDirectory}\bin\Otp.Web.XML").Exists)
                {
                    c.IncludeXmlComments($@"{AppDomain.CurrentDomain.BaseDirectory}\bin\Otp.Web.xml");
                }
            }).EnableSwaggerUi();
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

        private void ConfigureRequestValidation()
        {
            Filters.Add(new FilterForInvalidRequestsAttribute());
        }
    }
}