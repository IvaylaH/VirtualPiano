namespace VirtualPiano.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using VirtualPiano.Common;
    using VirtualPiano.Data;
    using VirtualPiano.Data.Common.Repository;
    using VirtualPiano.Models;
    using VirtualPiano.Web.Areas.MusicOpennings.ViewModels;
    using VirtualPiano.Web.Areas.MusicSheets.ViewModels;
    using VirtualPiano.Web.ViewModels.Home;

    public class HomeController : BaseController
    {

        public HomeController(IVirtualPianoData data)
            : base(data)
        {
        }

        //[OutputCache(Duration = 60 * 60)]
        public ActionResult Index()
        {
            var mostRecentMusicSheets = this.Data.MusicSheets.All()
                .OrderByDescending(sheet => sheet.CreatedOn)
                .Project()
                .To<CollectionOfSheetViewModel>()
                .Take(GlobalConstants.HomePageNumberOfMostRecentMusicItems)
                .ToList();

            var mostRecentMusicAds = this.Data.MusicAds.All()
                .OrderByDescending(ad => ad.CreatedOn)
                .Project()
                .To<OpenningBasicInfoViewModel>()
                .Take(GlobalConstants.HomePageNumberOfMostRecentMusicItems)
                .ToList();

            var newestUser = this.Data.Users.All()
                .OrderByDescending(user => user.CreatedOn)
                .Project()
                .To<UserBasicInfoViewModel>()
                .FirstOrDefault();

            var indexPage = new HomePageInfo()
            {
                MostRecentMusicAds = mostRecentMusicAds,
                MostRecentMusicSheets = mostRecentMusicSheets,
                MostRecentUser = newestUser
            };

            return View(indexPage);
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}