﻿using Ideal.IdentityManager;
using Ideal.Security.Certificates;
using IdentityServer3.Core.Configuration;
using Owin;

[assembly: OwinStartup(typeof(Startup))]


namespace Ideal.IdentityManager
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
					SiteName = "Embedded IdentityServer",
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