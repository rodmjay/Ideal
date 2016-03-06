using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IdentityServer3.Core.Models;

namespace Ideal.Identity
{
	public static class Clients
	{


		public static IEnumerable<Client> Get()
		{
			return new[]
			{
				new Client
				{
					ClientId = "authcocde",
					ClientName = "Ideal Identity - Auth Code",
					Flow = Flows.AuthorizationCode,
					AllowAccessToAllScopes = true,
					RedirectUris = new List<string>()
					{
						"http://localhost:49839/stscallback"
					},
					ClientSecrets = new List<Secret>()
					{
						new Secret("secret")
					}
				}
			};
		}
	}
}