using System.Web.Mvc;

namespace Ideal.Controllers
{
    public partial class HomeController
    {
        public ActionResult Index()
        {
            ViewBag.Title = "ASP.NET MVC + AngularJS";
            return View();
        }
    }
}