using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VirtualPiano.Web.Areas.MusicSheets.ViewModels
{
    public class MusicSheetDetailsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string[] Notes { get; set; }

        public string ArtistName { get; set; }

        public string CategoryName { get; set; }
    }
}