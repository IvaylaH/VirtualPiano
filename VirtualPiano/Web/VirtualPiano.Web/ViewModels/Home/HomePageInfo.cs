namespace VirtualPiano.Web.ViewModels.Home
{
    using System.Collections.Generic;
    using VirtualPiano.Web.Areas.MusicSheets.ViewModels;
    using VirtualPiano.Web.Areas.MusicOpennings.ViewModels;

    public class HomePageInfo
    {
        public IEnumerable<CollectionOfSheetViewModel> MostRecentMusicSheets { get; set; }

        public IEnumerable<OpenningBasicInfoViewModel> MostRecentMusicAds { get; set; }

        public UserBasicInfoViewModel MostRecentUser { get; set; }
    }
}