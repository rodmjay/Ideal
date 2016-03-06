using System.Collections.Generic;
using System.IdentityModel;

namespace Ideal.Security.Scopes
{
	public interface IScopeService
	{
		IEnumerable<Scope> Get();
	}
}