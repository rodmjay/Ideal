#region

using System.Configuration;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Ideal.Configuration;
using Ideal.Core.Data;
using Ideal.Core.Eventing;
using Ideal.Core.Settings;
using Ideal.Identity.Configuration;
using Ideal.Identity.Data;
using Ideal.Identity.Passwords;
using Ideal.Identity.Services;
using Ideal.Infrastructure.Data;
using Ideal.Infrastructure.Eventing;
using Ideal.Infrastructure.Repositories;

#endregion

namespace Ideal
{
    public static class DependencyConfig
    {
        public static void BuildDependencies()
        {
            var builder = new ContainerBuilder();

            // Register your MVC controllers.
            builder.RegisterControllers();

            // Register model binders that require DI.
            builder.RegisterModelBinders();
            builder.RegisterModelBinderProvider();

            // Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // Enable property injection into action filters.
            builder.RegisterFilterProvider();

            // config-based settings
            builder
                .Register(c => (SiteConfiguration)ConfigurationManager.GetSection("Ideal/Site"))
                .As<ISiteSettings>()
                .SingleInstance();

            builder
                .Register(c => (MembershipConfiguration)ConfigurationManager.GetSection("Ideal"))
                .As<IAccountSettings>()
                .SingleInstance();

            builder
                .Register(s => MessageBus.Instance)
                .As<IMessageBus>()
                .ExternallyOwned();

            builder
                .RegisterType<ClaimsAuthenticationService>()
                .As<IUserAuthenticationService>()
                .InstancePerRequest();

            builder
                .RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerRequest();

            builder
                .RegisterType<DatabaseFactory>()
                .As<IDatabaseFactory>()
                .InstancePerRequest();

            builder
                .RegisterType<MembershipService>()
                .As<IMembershipService>()
                .InstancePerRequest();

            builder
                .RegisterType<UserRepository>()
                .As<IUserRepository>()
                .InstancePerRequest();

            builder
                .RegisterType<NoopPasswordService>()
                .As<IPasswordService>()
                .InstancePerRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}