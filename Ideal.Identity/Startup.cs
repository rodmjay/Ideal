using Ideal.Identity;
using Ideal.IdentityManager;
using Ideal.Security.Certificates;
using IdentityServer3.Core.Configuration;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]


namespace Ideal.Identity
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{

			string connectionString = "MembershipReboot";
			app.Map("/connect", idsrvApp =>
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
		}

	}
}