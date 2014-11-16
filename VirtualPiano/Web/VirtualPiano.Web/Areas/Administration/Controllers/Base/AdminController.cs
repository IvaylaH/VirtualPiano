namespace VirtualPiano.Web.Areas.Administration.Controllers.Base
{
    using System.Web.Mvc;

    using VirtualPiano.Common;
    using VirtualPiano.Data;
    using VirtualPiano.Web.Controllers;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class AdminController : BaseController
    {
        public AdminController(IVirtualPianoData data)
            : base(data)
        {
        }
    }
}