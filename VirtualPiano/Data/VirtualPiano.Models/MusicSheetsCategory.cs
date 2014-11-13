namespace VirtualPiano.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using VirtualPiano.Data.Common.Models;

    public class MusicSheetsCategory
    {
        private ICollection<MusicSheet> musicSheets;

        public MusicSheetsCategory()
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
