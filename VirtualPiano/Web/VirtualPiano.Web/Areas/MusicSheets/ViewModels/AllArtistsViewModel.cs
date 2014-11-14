namespace VirtualPiano.Web.Areas.MusicSheets.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using VirtualPiano.Models;
    using VirtualPiano.Web.Infrastructure.Mapping;

    public class AllArtistsViewModel : IMapFrom<Artist>, IHaveCustomMappings
    {
        private IEnumerable<ArtistsCollectionOfSheetViewModel> musicSheets;

        public AllArtistsViewModel()
        {
            this.musicSheets = new HashSet<ArtistsCollectionOfSheetViewModel>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<ArtistsCollectionOfSheetViewModel> MusicSheets
        {
            get { return this.musicSheets; }
            set { this.musicSheets = value; }
        }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Artist, AllArtistsViewModel>()
                .ForMember(dest => dest.MusicSheets, opt => opt.MapFrom(artist => artist.MusicSheets.AsQueryable()
                    .OrderBy(sheet => sheet.Title)
                    .Select(sheets => new ArtistsCollectionOfSheetViewModel()
                    {
                        Id = sheets.Id,
                        Title = sheets.Title
                    })));
        }
    }
}