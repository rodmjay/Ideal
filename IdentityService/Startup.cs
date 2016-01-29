using System.Web.Mvc;
using Ideal.Core.Settings;
using Ideal.Identity.Settings;
using IdentityServer3.Core.Configuration;
using IdentityService.Config;
using Owin;

namespace IdentityService
{
    public class Startup
    {
        private ISecureTokenServiceSettings ServiceSettings
            => DependencyResolver.Current.GetService<ISecureTokenServiceSettings>();

        private ISiteSettings SiteSettings 
            => DependencyResolver.Current.GetService<ISiteSettings>();

        public void Configuration(IAppBuilder app)
        {
            app.Map("/identity", idsrvApp =>
            {
                // see pluralsight
                var idServerServiceFactory = new IdentityServerServiceFactory()
                    .UseInMemoryClients(Clients.Get())
                    .UseInMemoryScopes(Scopes.Get())
                    .UseInMemoryUsers(Users.Get());

                var options = new IdentityServerOptions{
                    Factory = idServerServiceFactory,
                    SiteName = SiteSettings.WebsiteName,
                    IssuerUri = ServiceSettings.IssuerUri,
                    PublicOrigin = ServiceSettings.RootUri
                };

                idsrvApp.UseIdentityServer(options);
            });
        }
    }
}