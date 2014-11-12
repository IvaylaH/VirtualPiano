namespace VirtualPiano.Web.Areas.MusicSheets.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public class MusicSheetsListAllModelView
    {
        public int Id { get; set; }

        [Display(Name = "Title")]
        [Required]
        public string Title { get; set; }

        [Display(Name = "Artist")]
        [Required]
        public string ArtistName { get; set; }

        [Display(Name = "Category")]
        [Required]
        public string CategoryName { get; set; }
    }
}