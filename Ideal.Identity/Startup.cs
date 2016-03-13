using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Ideal.Identity;
using Ideal.IdentityManager;
using Ideal.Security.Certificates;
using IdentityModel.Client;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using AuthenticationOptions = IdentityServer3.Core.Configuration.AuthenticationOptions;

[assembly: OwinStartup(typeof(Startup))]
namespace Ideal.Identity
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			string connectionString = "MembershipReboot";
			app.Map("/identity", idsrvApp =>
			{
				var idSvrFactory = Factory.Configure();
				idSvrFactory.ConfigureCustomUserService(connectionString);

				idsrvApp.UseIdentityServer(new IdentityServerOptions
				{
					SiteName = "Ideal Identity",
					SigningCertificate = Certificate.Get(),
					Factory = idSvrFactory,
					AuthenticationOptions = new AuthenticationOptions
					{
						EnablePostSignOutAutoRedirect = true
					}
				});
			});

			//app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

			//app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
			//{
			//	Authority = IdealConstants.STSEndpoint,

			//	ClientId = IdealConstants.ClientId,
			//	Scope = "openid profile roles sampleApi",
			//	ResponseType = "id_token token",
			//	RedirectUri = IdealConstants.ClientOrigin,

			//	SignInAsAuthenticationType = "Cookies",
			//	UseTokenLifetime = false,

			//	Notifications = new OpenIdConnectAuthenticationNotifications
			//	{
			//		SecurityTokenValidated = async n =>
			//		{
			//			var nid = new ClaimsIdentity(
			//				n.AuthenticationTicket.Identity.AuthenticationType,
			//				IdentityServer3.Core.Constants.ClaimTypes.GivenName,
			//				IdentityServer3.Core.Constants.ClaimTypes.Role);

			//			// get userinfo data
			//			var userInfoClient = new UserInfoClient(
			//				new Uri(IdealConstants.STSUserInfoEndpoint),
			//				n.ProtocolMessage.AccessToken);

			//			var userInfo = await userInfoClient.GetAsync();
			//			userInfo.Claims.ToList().ForEach(ui => nid.AddClaim(new Claim(ui.Item1, ui.Item2)));

			//			// keep the id_token for logout
			//			nid.AddClaim(new Claim("id_token", n.ProtocolMessage.IdToken));

			//			// add access token for sample API
			//			nid.AddClaim(new Claim("access_token", n.ProtocolMessage.AccessToken));

			//			// keep track of access token expiration
			//			nid.AddClaim(new Claim("expires_at", DateTimeOffset.Now.AddSeconds(int.Parse(n.ProtocolMessage.ExpiresIn)).ToString()));

			//			// add some other app specific claim
			//			nid.AddClaim(new Claim("app_specific", "some data"));

			//			n.AuthenticationTicket = new AuthenticationTicket(
			//				nid,
			//				n.AuthenticationTicket.Properties);
			//		},

			//		RedirectToIdentityProvider = n =>
			//		{
			//			if (n.ProtocolMessage.RequestType == OpenIdConnectRequestType.LogoutRequest)
			//			{
			//				var idTokenHint = n.OwinContext.Authentication.User.FindFirst("id_token");

			//				if (idTokenHint != null)
			//				{
			//					n.ProtocolMessage.IdTokenHint = idTokenHint.Value;
			//				}
			//			}

			//			return Task.FromResult(0);
			//		}
			//	}
			//});
		}

	}
}