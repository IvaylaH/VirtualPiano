namespace VirtualPiano.Web.Areas.MusicSheets.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using VirtualPiano.Models;
    using VirtualPiano.Web.Infrastructure.Mapping;

    public class CategoriesViewModel : IMapFrom<MusicSheetsCategory>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<CollectionOfSheetViewModel> MusicSheets { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<MusicSheetsCategory, CategoriesViewModel>()
                .ForMember(dest => dest.MusicSheets, opt => opt.MapFrom(category => category.MusicSheets
                    .AsQueryable()
                    .OrderBy(sheet => sheet.Title)
                    .Select(sheets => new CollectionOfSheetViewModel()
                    {
                        Id = sheets.Id,
                        Title = sheets.Title
                    })));
        }
    }
}