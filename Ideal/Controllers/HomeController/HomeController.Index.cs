using System.Web.Mvc;

namespace Ideal.Controllers
{
    public partial class HomeController
    {
        public ActionResult Index()
        {
            ViewBag.PageTitle = "ASP.NET MVC + AngularJS";
            return View();
        }
    }
}