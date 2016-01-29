using System.Configuration;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Ideal.Identity.Configuration;
using Ideal.Identity.Settings;

namespace IdentityService.Autofac
{
    public static class DependencyBuilder
    {
        public static void BuildDependencies()
        {
            var builder = new ContainerBuilder();

            // config-based settings
            builder
                .Register(c => (SecureTokenServiceConfiguration)ConfigurationManager.GetSection("Ideal/Site"))
                .As<ISecureTokenServiceSettings>()
                .SingleInstance();
      
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}