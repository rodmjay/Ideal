#region

using System.Configuration;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Ideal.Configuration;
using Ideal.Core.Common.Membership.PasswordPolicies;
using Ideal.Core.Interfaces.Data;
using Ideal.Core.Interfaces.Eventing;
using Ideal.Core.Interfaces.Membership;
using Ideal.Core.Interfaces.Notifications;
using Ideal.Core.Interfaces.Service;
using Ideal.Core.Interfaces.Site;
using Ideal.Core.Services;
using Ideal.Infrastructure.Data;
using Ideal.Infrastructure.Eventing;
using Ideal.Infrastructure.Repositories;
using Ideal.Membership;
using Ideal.Membership.Configuration;
using Ideal.Security.Authentication;

#endregion

namespace Ideal.Autofac
{
    public static class DependencyBuilder
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
                .Register(c => (ConfigSiteSettings)ConfigurationManager.GetSection("Ideal/Site"))
                .As<ISiteSettings>()
                .SingleInstance();

            builder
                .Register(c => (ConfigMembershipSettings)ConfigurationManager.GetSection("Ideal"))
                .As<IMembershipSettings>()
                .SingleInstance();

            builder
                .Register(s => MessageBus.Instance)
                .As<IMessageBus>()
                .ExternallyOwned();

            builder
                .RegisterType<ClaimsAuthenticationService>()
                .As<IAuthenticationService>()
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
                .RegisterType<UserAccountService>()
                .As<IUserAccountService>()
                .InstancePerRequest();

            builder
                .RegisterType<UserRepository>()
                .As<IUserRepository>()
                .InstancePerRequest();

            builder
                .RegisterType<NoopPasswordPolicy>()
                .As<IPasswordPolicy>()
                .InstancePerRequest();

            builder
                .RegisterType<NoopNotificationService>()
                .As<INotificationService>()
                .InstancePerRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}