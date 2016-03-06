using System.Collections.Generic;
using IdentityServer3.Core.Models;

namespace Ideal.Security.Clients
{
	public interface IClientService
	{
		IEnumerable<Client> Get();
	}
}