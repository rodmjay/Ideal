using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using IdentityModel.Client;

namespace Ideal
{
	public static class GPNHttpClient
	{
		public static HttpClient GetClient()
		{
			HttpClient client = new HttpClient();
			var accessToken = RequestTokenAuthorizationCode();
			if(accessToken!=null)
				client.SetBearerToken(accessToken);
			client.BaseAddress = new Uri(IdealConstants.GPNAPIOrigin);

			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			return client;
		}

		private static string RequestTokenAuthorizationCode()
		{
			var cookie = HttpContext.Current.Request.Cookies.Get("GPNCookie");
			if (cookie != null && cookie["access_token"] == null)
			{
				return cookie["access_token"];
			}

			var authorizeRequest = new AuthorizeRequest(IdealConstants.GPNSTSAuthorizationEndpoint);

			var state = HttpContext.Current.Request.Url.OriginalString;

			var url = authorizeRequest.CreateAuthorizeUrl(IdealConstants.EdgeClientId, "code", "sampleApi",
				IdealConstants.EdgeMVCSTSCallback, state);

			HttpContext.Current.Response.Redirect(url);
			return null;
		}
	}
}