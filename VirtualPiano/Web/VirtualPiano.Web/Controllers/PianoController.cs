namespace VirtualPiano.Web.Controllers
{
    using System.Web.Mvc;
    using VirtualPiano.Data;
    
    public class PianoController : BaseController
    {
        public PianoController(IVirtualPianoData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}