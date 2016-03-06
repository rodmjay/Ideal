using Ideal.Identity;
using Ideal.IdentityManager;
using Ideal.Security.Certificates;
using IdentityServer3.Core.Configuration;
using Microsoft.Owin;
using Microsoft.Owin.Security.Google;
using Owin;

[assembly: OwinStartup(typeof(Startup))]


namespace Ideal.Identity
{
	public class Startup
	{
		public void Config(IAppBuilder app)
		{
			string connectionString = "MembershipReboot";
			app.Map("/identity", idsrvApp =>
			{
				var idSvrFactory = Factory.Configure();
				idSvrFactory.ConfigureCustomUserService(connectionString);

				idsrvApp.UseIdentityServer(new IdentityServerOptions
				{
					SiteName = "Embedded IdentityServer",
					SigningCertificate = Certificate.Get(),
					Factory = idSvrFactory,
					AuthenticationOptions = new AuthenticationOptions
					{
						EnablePostSignOutAutoRedirect = true,
						IdentityProviders = ConfigureIdentityProviders
					}
				});
			});
		}

		private void ConfigureIdentityProviders(IAppBuilder app, string signInAsType)
		{
			app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions
			{
				AuthenticationType = "Google",
				Caption = "Sign-in with Google",
				SignInAsAuthenticationType = signInAsType,

				ClientId = "701386055558-9epl93fgsjfmdn14frqvaq2r9i44qgaa.apps.googleusercontent.com",
				ClientSecret = "3pyawKDWaXwsPuRDL7LtKm_o"
			});
		}
	}
}