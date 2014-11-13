namespace VirtualPiano.Web.Areas.MusicSheets.ViewModels
{
    using System.Collections.Generic;

    using AutoMapper;

    using VirtualPiano.Models;
    using VirtualPiano.Web.Infrastructure.Mapping;

    public class AllArtistsViewModel : IMapFrom<Artist>
    {
        public string Name { get; set; }

        public ICollection<ArtistsCollectionOfSheetViewModel> Songs { get; set; }
    }
}