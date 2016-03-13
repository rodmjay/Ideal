using System.Threading.Tasks;
using System.Web.Mvc;
using Ideal.HttpClients;

namespace Ideal.Controllers
{
	public class ValuesController : Controller
	{
		// GET: Identity
		public async Task<ActionResult> Index()
		{
			var client = IdealHttpClient.GetClient();

			var x = await client.GetAsync("api/values").ConfigureAwait(false);
			if (x.IsSuccessStatusCode)
			{
				var json = await x.Content.ReadAsStringAsync().ConfigureAwait(false);
				return Content(json);
			}
			else return View("Error");
		}
	}
}