using System;

namespace Ideal.IdentityManager
{
	static class Extensions
	{
		public static Guid ToGuid(this string s)
		{
			Guid g;
			if (Guid.TryParse(s, out g))
			{
				return g;
			}

			return Guid.Empty;
		}
	}
}