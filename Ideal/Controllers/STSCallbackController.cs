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

	        var client = new TokenClient(IdealConstants.GPNSTSTokenEndpoint,
				IdealConstants.EdgeClientId,
				IdealConstants.EdgeClientSecret);

	        var tokenResponse = await client.RequestAuthorizationCodeAsync(
		        authCode,
				IdealConstants.EdgeMVCSTSCallback);

	        Response.Cookies["GPNCookie"]["access_token"] = tokenResponse.AccessToken;

            return Redirect(Request.QueryString["state"]);
        }
    }
}