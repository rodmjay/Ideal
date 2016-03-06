using System.Threading.Tasks;
using System.Web.Mvc;

namespace Ideal.Controllers
{
    public class IdentityController : Controller
    {
        // GET: Identity
        public async Task<ActionResult> Index()
        {
	        var client = GPNHttpClient.GetClient();

	        var x = await client.GetAsync("identity").ConfigureAwait(false);
	        if (x.IsSuccessStatusCode)
	        {
		        var identity = await x.Content.ReadAsStringAsync().ConfigureAwait(false);
		        return Content(identity);
	        }
			else return View("Error");
		}
    }
}