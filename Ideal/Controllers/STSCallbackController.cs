using System.Threading.Tasks;
using System.Web.Mvc;
using IdentityModel.Client;
using IdentityModel.Extensions;

namespace Ideal.Controllers
{
    public class STSCallbackController : Controller
    {
        public async Task<ActionResult> Index()
        {
	        var authCode = Request.QueryString["code"];

	        var client = new TokenClient(IdealConstants.STSTokenEndpoint,
				IdealConstants.ClientId,
				IdealConstants.ClientSecret);

	        var tokenResponse = await client.RequestAuthorizationCodeAsync(
		        authCode,
				IdealConstants.ClientCallbackUrl);

	        Response.Cookies["ideal.auth"]["access_token"] = tokenResponse.AccessToken;

            return Redirect(Request.QueryString["state"]);
        }
    }
}