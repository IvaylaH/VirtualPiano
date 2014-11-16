namespace VirtualPiano.Web.Areas.MusicOpennings.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using VirtualPiano.Common;
    using VirtualPiano.Data;
    using VirtualPiano.Web.Controllers;
    using VirtualPiano.Web.Areas.MusicOpennings.ViewModels;

    public class SideBarController : BaseController
    {
        public SideBarController(IVirtualPianoData data)
            : base(data)
        {
        }

        [ChildActionOnly]
        //[OutputCache(Duration = 10 * 60)]
        public ActionResult Index()
        {
            var sideBar = new SideBarViewModel()
            {
                MostRecentMusicAds = this.Data.MusicAds.All()
                    .OrderByDescending(sheet => sheet.CreatedOn)
                    .Project()
                    .To<OpenningBasicInfoViewModel>()
                    .Take(GlobalConstants.DefaultNumberOfMostRecentMusicItems)
                    .ToList()
            };

            return this.PartialView("_SideBarMusicAds", sideBar);
        }
    }
}