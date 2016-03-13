using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Web;
using IdentityModel.Client;

namespace Ideal.HttpClients
{
	public static class IdealHttpClient
	{
		public static HttpClient GetClient()
		{
			HttpClient client = new HttpClient();
			var token = RequestTokenAuthorizationCode();
			if(!string.IsNullOrEmpty(token))
				client.SetBearerToken(token);
			client.BaseAddress = new Uri(IdealConstants.ApiOriginUrl);

			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			return client;
		}

		private static string RequestTokenAuthorizationCode()
		{
			var cookie = HttpContext.Current.Request.Cookies.Get("ideal.auth");
			if (cookie?["access_token"] != null)
			{
				return cookie["access_token"];
			}

			var authorizeRequest = new AuthorizeRequest(IdealConstants.STSAuthorizationEndpoint);

			var state = HttpContext.Current.Request.Url.OriginalString;

			var url = authorizeRequest.CreateAuthorizeUrl(IdealConstants.ClientId, "code", "sampleApi",
				IdealConstants.ClientCallbackUrl, state);

			HttpContext.Current.Response.Redirect(url);
			return null;
		}
	}
}