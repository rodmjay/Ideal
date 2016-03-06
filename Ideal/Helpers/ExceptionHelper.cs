using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace Ideal.Helpers
{
	public static class ExceptionHelper
	{
		public static Exception GetExceptionFromResponse(HttpResponseMessage response)
		{
			if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.Unauthorized)
			{
				return new Exception("You're not allowed to do that");
			}
			else
			{
				return new Exception("Something went wrong ");
			}
		}
	}
}