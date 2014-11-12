﻿namespace VirtualPiano.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Artist
    {
        private ICollection<MusicSheet> musicSheets;

        public Artist()
        {
            this.musicSheets = new HashSet<MusicSheet>();    
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<MusicSheet> MusicSheets 
        {
            get { return this.musicSheets; }
            set { this.musicSheets = value; }
        }
    }
}
