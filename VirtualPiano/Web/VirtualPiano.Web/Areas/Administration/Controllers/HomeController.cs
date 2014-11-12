namespace VirtualPiano.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        // GET: Administration/Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Navigation()
        {
            return View();
        }
    }
}