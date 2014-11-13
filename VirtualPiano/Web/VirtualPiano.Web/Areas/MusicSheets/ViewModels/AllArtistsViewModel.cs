namespace VirtualPiano.Web.Areas.MusicSheets.ViewModels
{
    using System.Collections.Generic;

    using AutoMapper;

    using VirtualPiano.Models;
    using VirtualPiano.Web.Infrastructure.Mapping;

    public class AllArtistsViewModel : IMapFrom<Artist>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public ICollection<string> SongsNames { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<MusicSheet, AllArtistsViewModel>()
                .ForMember(dest => dest.SongsNames, opt => opt.MapFrom(sheet => sheet.Title));
        }
    }
}