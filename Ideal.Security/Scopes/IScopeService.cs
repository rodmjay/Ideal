
using System.Collections.Generic;
using IdentityServer3.Core.Models;

namespace Ideal.Security.Scopes
{
	public interface IScopeService
	{
		IEnumerable<Scope> Get();
	}
}