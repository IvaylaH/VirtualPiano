namespace VirtualPiano.Web.Areas.MusicOpennings.InputViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class InputMusicOpenningViewModel
    {
        [Display(Name = "Title")]
        [Required]
        [StringLength(maximumLength: 150, MinimumLength = 10)]
        public string Title { get; set; }

        [Display(Name = "Content")]
        [Required]
        [StringLength(maximumLength: 1000, MinimumLength = 30)]
        [UIHint("Notes")]
        public string Content { get; set; }

        [Display(Name = "Category")]
        public string Category { get; set; }
    }
}