namespace VirtualPiano.Web.Areas.MusicSheets.ViewModels
{
    using System.Collections.Generic;

    public class SideBarViewModel
    {
        public IEnumerable<CollectionOfSheetViewModel> MostRecentSongs { get; set; }
    }
}