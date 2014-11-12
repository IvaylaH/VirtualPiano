namespace VirtualPiano.Web.Areas.MusicSheets.InputModels
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class InputMusicSheetViewModel
    {
        [Display(Name = "Song Title")]
        [Required]
        public string Title { get; set; }

        [Display(Name = "Artist/Composer")]
        [Required]
        public string Artist { get; set; }

        [Display(Name = "Music Genre")]
        public string Category { get; set; }

        [AllowHtml]
        [Display(Name = "Song Notes")]
        [Required]
        [UIHint("Notes")]
        public string Notes { get; set; }
    }
}