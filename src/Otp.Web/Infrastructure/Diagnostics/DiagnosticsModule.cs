using System.Web.Http.ExceptionHandling;
using Autofac;
using JE.ApiExceptions.WebApi.NLog;

namespace Otp.Web.Infrastructure.Diagnostics
{
    public class DiagnosticsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<NLogExceptionLogger>().As<IExceptionLogger>();
            base.Load(builder);
        }
    }
}