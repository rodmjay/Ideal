using System.Collections.Generic;
using IdentityServer3.Core.Models;

namespace Ideal.Security.Clients
{
	public class ClientService : IClientService
	{
		public IEnumerable<Client> Get()
		{
			return new[]
			{
				new Client
				{
					ClientId = IdealConstants.ClientId,
					ClientName = "Ideal Identity",
					Enabled = true,
					Flow = Flows.AuthorizationCode,
					AllowAccessToAllScopes = true,
					RedirectUris = new List<string>()
					{
						IdealConstants.ClientCallbackUrl
					},
					ClientSecrets = new List<Secret>()
					{
						new Secret(IdealConstants.ClientSecret)
					}
				}
			};
		}
	}
}
