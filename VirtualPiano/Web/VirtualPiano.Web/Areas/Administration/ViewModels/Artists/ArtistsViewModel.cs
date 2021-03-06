﻿namespace VirtualPiano.Web.Areas.Administration.ViewModels.Artists
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using VirtualPiano.Models;
    using VirtualPiano.Web.Infrastructure.Mapping;

    public class ArtistsViewModel : IMapFrom<Artist>
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Artist/Composer Name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Modified On")]
        public DateTime? ModifiedOn { get; set; }

        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }
    }
}