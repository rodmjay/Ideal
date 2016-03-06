using Ideal;
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

			//app.UseResourceAuthorization(new AuthorizationManager());

			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationType = "Cookies"
			});


			//app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
			//{
			//	Authority = "https://gpn-identity.azurewebsites.net/identity",

			//	ClientId = "mvc",
			//	Scope = "openid profile roles sampleApi",
			//	ResponseType = "id_token token",
			//	RedirectUri = "https://gpn-identity.azurewebsites.net/identity",

			//	SignInAsAuthenticationType = "Cookies",
			//	UseTokenLifetime = false,

			//	Notifications = new OpenIdConnectAuthenticationNotifications
			//	{
			//		SecurityTokenValidated = async n =>
			//		{
			//			var nid = new ClaimsIdentity(
			//				n.AuthenticationTicket.Identity.AuthenticationType,
			//				"given_name",
			//				"role");

			//			// get userinfo data
			//			var userInfoClient = new UserInfoClient(
			//				new Uri(n.Options.Authority + "/connect/userinfo"),
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