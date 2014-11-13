namespace VirtualPiano.Web.Areas.MusicSheets.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public class ArtistsCollectionOfSheetViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Song Title")]
        public string Title { get; set; }
    }
}