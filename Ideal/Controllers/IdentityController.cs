using System.Threading.Tasks;
using System.Web.Mvc;
using Ideal.HttpClients;

namespace Ideal.Controllers
{
	public class IdentityController : Controller
	{
		// GET: Identity
		public async Task<ActionResult> Index()
		{
			var client = IdealHttpClient.GetClient();

			var x = await client.GetAsync("identity").ConfigureAwait(false);
			if (x.IsSuccessStatusCode)
			{
				var identity = await x.Content.ReadAsStringAsync().ConfigureAwait(false);
				return Content("Worked");
			}
			else return View("Error");
		}
	}
}