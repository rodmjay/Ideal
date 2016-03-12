using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using IdentityServer3.Core.Extensions;

namespace Ideal.Identity.Controllers
{
	public class LoginController : Controller
	{
		[Route("core/custom/login")]
		public ActionResult Index(string id)
		{
			return View();
		}

		[Route("core/custom/login")]
		[HttpPost]
		public async Task<ActionResult> Index(string id, string sub, string name)
		{
			var env = Request.GetOwinContext().Environment;
			env.IssueLoginCookie(new IdentityServer3.Core.Models.AuthenticatedLogin
			{
				Subject = sub,
				Name = name,
			});

			var msg = env.GetSignInMessage(id);
			var returnUrl = msg.ReturnUrl;

			env.RemovePartialLoginCookie();

			return Redirect(returnUrl);
		}
	}
}