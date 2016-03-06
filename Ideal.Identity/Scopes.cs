using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IdentityServer3.Core.Models;

namespace Ideal.Identity
{
	public static class Scopes
	{
		public static IEnumerable<Scope> Get()
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