using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Ideal;
using Ideal.Controllers;
using Ideal.Security;
using Ideal.Security.Authorization;
using IdentityModel.Client;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
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