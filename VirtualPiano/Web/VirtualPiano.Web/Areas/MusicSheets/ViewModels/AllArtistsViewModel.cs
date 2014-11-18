namespace VirtualPiano.Web.Areas.MusicSheets.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using VirtualPiano.Models;
    using VirtualPiano.Web.Infrastructure.Mapping;

    public class AllArtistsViewModel : IMapFrom<Artist>, IHaveCustomMappings
    {
        private IEnumerable<CollectionOfSheetViewModel> musicSheets;

        public AllArtistsViewModel()
        {
            this.musicSheets = new HashSet<CollectionOfSheetViewModel>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual IEnumerable<CollectionOfSheetViewModel> MusicSheets
        {
            get { return this.musicSheets; }
            set { this.musicSheets = value; }
        }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Artist, AllArtistsViewModel>()
                .ForMember(dest => dest.MusicSheets, opt => opt.MapFrom(artist => artist.MusicSheets
                    .AsQueryable()
                    .Where(sheet => !sheet.IsDeleted)
                    .OrderBy(sheet => sheet.Title)
                    .Select(sheets => new CollectionOfSheetViewModel()
                    {
                        Id = sheets.Id,
                        Title = sheets.Title
                    })));
        }
    }
}