﻿namespace VirtualPiano.Web.Areas.MusicSheets.ViewModels
{
    using System;
    using System.Linq;

    using AutoMapper;

    using VirtualPiano.Models;
    using VirtualPiano.Web.Infrastructure.Mapping;

    public class MusicSheetDetailsViewModel : IMapFrom<MusicSheet>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string[] Notes { get; set; }

        public MusicSheetsArtistViewModel Artist { get; set; }

        public MusicSheetsCategoryViewModel Category { get; set; }
    }
}