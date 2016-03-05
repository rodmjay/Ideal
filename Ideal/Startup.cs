using Ideal;
using IdentityServer3.AccessTokenValidation;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace Ideal
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
			{
				Authority = "https://gpn-identity.azurewebsites.net/identity",
				RequiredScopes = new[] { "sampleApi" }
			});
		}
	}
}