using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ideal.Identity.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return Content("working");
			
		}
	}
}