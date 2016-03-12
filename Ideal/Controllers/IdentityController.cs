using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace Ideal.Controllers
{
	public class IdentityController : Controller
	{
		// GET: CallApi/ClientCredentials
		public async Task<ActionResult> Index()
		{
			var response = await GetTokenAsync();
			var result = await CallApi(response.AccessToken);

			ViewBag.Json = result;
			return View("ShowApiResult");
		}

		// GET: CallApi/UserCredentials
		public async Task<ActionResult> UserCredentials()
		{
			var user = User as ClaimsPrincipal;
			var token = user.FindFirst("access_token").Value;
			var result = await CallApi(token);

			ViewBag.Json = result;
			return View("ShowApiResult");
		}

		private async Task<string> CallApi(string token)
		{
			var client = new HttpClient();
			client.SetBearerToken(token);

			var json = await client.GetStringAsync(IdealConstants.ApiOriginUrl +"/identity");
			return JArray.Parse(json).ToString();
		}

		private async Task<TokenResponse> GetTokenAsync()
		{
			var client = new TokenClient(
				IdealConstants.STSTokenEndpoint,
				IdealConstants.ClientId,
				IdealConstants.ClientSecret);

			return await client.RequestClientCredentialsAsync("sampleApi");
		}
	}
}