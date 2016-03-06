using IdentityServer3.Core.Configuration;
using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;

namespace Ideal.Identity
{
	public class Startup
	{
		public void Config(IAppBuilder app)
		{
			app.Map("/", idsrvApp =>
			{
				var idSvrFactory = Factory.Configure();
				idSvrFactory.ConfigureCustomUserService(connectionString);

				idsrvApp.UseIdentityServer(new IdentityServerOptions
				{
					SiteName = "Embedded IdentityServer",
					SigningCertificate = Certificate.Get(),
					Factory = idSvrFactory,
					AuthenticationOptions = new IdentityServer3.Core.Configuration.AuthenticationOptions
					{
						EnablePostSignOutAutoRedirect = true,
						IdentityProviders = ConfigureIdentityProviders
					}
				});
			});
		}
	}
}