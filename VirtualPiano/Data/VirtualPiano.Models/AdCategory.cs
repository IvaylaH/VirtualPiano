namespace VirtualPiano.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using VirtualPiano.Data.Common.Models;

    public class AdCategory
    {
        private ICollection<MusicAd> musicAds;

        public AdCategory()
        {
            this.musicAds = new HashSet<MusicAd>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 2)]
        public string Name { get; set; }

        public virtual ICollection<MusicAd> MusicAds 
        {
            get { return this.musicAds; }
            set { this.musicAds = value; }
        }
    }
}
