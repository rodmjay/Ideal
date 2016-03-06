using System.Collections.Generic;
using System.IdentityModel;

namespace Ideal.Security.Scopes
{
	public class ScopeService : IScopeService
	{
		public IEnumerable<Scope> Get()
		{
			return new[]
			{
				new Scope()
				{

				}
			};
		}
	}
}
