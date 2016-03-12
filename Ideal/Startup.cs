using Ideal;
using Ideal.Controllers;
using Ideal.Security;
using Ideal.Security.Authorization;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace Ideal
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{

			app.UseResourceAuthorization(new AuthorizationManagerX());

			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationType = "Cookies"
			});
		}
	}
}