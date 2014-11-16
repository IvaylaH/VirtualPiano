namespace VirtualPiano.Web.Areas.MusicSheets.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using VirtualPiano.Common;
    using VirtualPiano.Data;
    using VirtualPiano.Web.Areas.MusicSheets.ViewModels;
    using VirtualPiano.Web.Controllers;

    public class SideBarController : BaseController
    {
        public SideBarController(IVirtualPianoData data)
            : base(data)
        {
        }

        [ChildActionOnly]
        [OutputCache(Duration = 10 * 60)]
        public ActionResult Index()
        {
            var sideBar = new SideBarViewModel()
            {
                MostRecentSongs = this.Data.MusicSheets.All()
                    .OrderByDescending(sheet => sheet.CreatedOn)
                    .Project()
                    .To<CollectionOfSheetViewModel>()
                    .Take(GlobalConstants.DefaultNumberOfMostRecentMusicItems)
                    .ToList()
            };

            return this.PartialView("_SideBarMusicSheets", sideBar);
        }
    }
}