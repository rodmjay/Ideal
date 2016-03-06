using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ideal.Controllers
{
	[Authorize]
    public class ProtectedController : Controller
    {
        // GET: Protected
        public ActionResult Index()
        {
            return View();
        }
    }
}