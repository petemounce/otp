using System.Linq;
using System.Reflection;
using System.Web.Http.Validation;
using Autofac;
using FluentValidation;
using FluentValidation.WebApi;
using Otp.Web.OneTimePasswords;
using Module = Autofac.Module;

namespace Otp.Web.Infrastructure
{
    public class AutofacFluentValidationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterValidatorsInAssembly(builder, typeof (RequestValidationFulesForPasswordVerification).Assembly);
            builder.RegisterType<FluentValidationModelValidatorProvider>().As<ModelValidatorProvider>();
            builder.RegisterType<AutofacValidatorFactory>().As<IValidatorFactory>().SingleInstance();
            base.Load(builder);
        }

        private static void RegisterValidatorsInAssembly(ContainerBuilder builder, Assembly assembly)
        {
            builder.RegisterAssemblyTypes(assembly)
                   .Where(t => t.GetInterfaces().Any(x => x.Name.Contains("IValidator")))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();
//            var findValidatorsInAssembly = AssemblyScanner.FindValidatorsInAssembly(assembly);
//            foreach (var item in findValidatorsInAssembly)
//            {
//                builder
//                    .RegisterType(item.ValidatorType)
//                    .Keyed<IValidator>(item.InterfaceType)
//                    .As<IValidator>();
//            }
        }
    }
}