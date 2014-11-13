namespace VirtualPiano.Web.Controllers
{
    using System.Web.Mvc;

    using VirtualPiano.Data;
    using VirtualPiano.Models;

    public abstract class BaseController : Controller
    {
        public BaseController(IVirtualPianoData data)
        {
            this.Data = data;
        }

        protected IVirtualPianoData Data { get; set; }

        protected ApplicationUser CurrentUser { get; set; }
    }
}