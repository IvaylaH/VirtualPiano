using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VirtualPiano.Web.Areas.MusicOpennings.Controllers
{
    public class OpenningsController : Controller
    {
        // GET: MusicOpennings/Opennings
        public ActionResult Index()
        {
            return View();
        }
    }
}