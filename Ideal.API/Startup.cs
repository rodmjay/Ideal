using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Ideal.API;
using IdentityServer3.AccessTokenValidation;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace Ideal.API
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			// token validation
			app.UseIdentityServerBearerTokenAuthentication(
				new IdentityServerBearerTokenAuthenticationOptions
				{
					Authority = IdealConstants.STSEndpoint,
					RequiredScopes = new[] { "sampleApi" }
				});

			// web api configuration
			var config = WebApiConfig.Register();

			app.UseWebApi(config);
		}
	}
}