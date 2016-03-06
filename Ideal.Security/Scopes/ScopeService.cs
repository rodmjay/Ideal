using System.Collections.Generic;
using IdentityServer3.Core.Models;

namespace Ideal.Security.Scopes
{
	public class ScopeService : IScopeService
	{
		public IEnumerable<Scope> Get()
		{
			return new Scope[]
			{
				new Scope
				{
					Enabled = true,
					Name = "roles",
					Type = ScopeType.Identity,
					Claims = new List<ScopeClaim>
					{
						new ScopeClaim("role")
					}
				},
				new Scope
				{
					Enabled = true,
					DisplayName = "Sample API",
					Name = "sampleApi",
					Description = "Access to a sample API",
					Type = ScopeType.Resource,

					Claims = new List<ScopeClaim>
					{
						new ScopeClaim("role")
					}
				},
				StandardScopes.OpenId,
				StandardScopes.Profile,
				StandardScopes.Email,
				StandardScopes.OfflineAccess,
				new Scope
				{
					Name = "read",
					DisplayName = "Read data",
					Type = ScopeType.Resource,
					Emphasize = false,
				},
				new Scope
				{
					Name = "write",
					DisplayName = "Write data",
					Type = ScopeType.Resource,
					Emphasize = true,
				},
				new Scope
				{
					Name = "forbidden",
					DisplayName = "Forbidden scope",
					Type = ScopeType.Resource,
					Emphasize = true
				},
				new Scope
				{
					Name ="admin",
					DisplayName = "Admin data",
					Type = ScopeType.Resource,
					Emphasize = false
				}
			 };
		}
	}
}
