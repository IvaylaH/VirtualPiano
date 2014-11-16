namespace VirtualPiano.Web.Areas.MusicOpennings.ViewModels
{
    using System.Collections.Generic;

    public class SideBarViewModel
    {
        public IEnumerable<OpenningBasicInfoViewModel> MostRecentMusicAds { get; set; }
    }
}