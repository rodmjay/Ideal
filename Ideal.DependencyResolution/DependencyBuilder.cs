#region

using System.Configuration;
using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Ideal.Core.Interfaces.Eventing;
using Ideal.Core.Interfaces.Membership;
using Ideal.Core.Interfaces.Site;
using Ideal.Infrastructure.Configuration;
using Ideal.Infrastructure.Eventing;

#endregion

namespace Ideal.DependencyResolution
{
    public static class DependencyBuilder
    {
        public static void BuildDependencies()
        {
            var builder = new ContainerBuilder();

            // Register your MVC controllers.
            builder.RegisterControllers(Assembly.GetCallingAssembly());

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(Assembly.GetCallingAssembly());
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            // config-based settings
            builder
                .Register(s => AppConfig.Instance.Site)
                .As<ISiteSettings>()
                .ExternallyOwned();

            builder
                .Register(s => (ConfigMembershipSettings) ConfigurationManager.GetSection("Ideal/membership"))
                .As<IMembershipSettings>()
                .SingleInstance();

            builder
                .Register(s => MessageBus.Instance)
                .As<IMessageBus>()
                .ExternallyOwned();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}