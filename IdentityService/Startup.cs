using IdentityServer3.Core.Configuration;
using IdentityService.Config;
using Owin;

namespace IdentityService
{
    public class Startup
    {
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
                    SiteName = "Ideal Identity",
                    IssuerUri = "https://wcpro/identity",
                    PublicOrigin = "https://localhost:44300/"
                };

                idsrvApp.UseIdentityServer(options);
            });
        }
    }
}