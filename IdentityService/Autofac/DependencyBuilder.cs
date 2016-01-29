using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Ideal.Core.Interfaces.Settings;
using Ideal.Membership.Configuration;

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