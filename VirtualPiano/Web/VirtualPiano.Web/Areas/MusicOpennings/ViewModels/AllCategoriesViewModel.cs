namespace VirtualPiano.Web.Areas.MusicOpennings.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using VirtualPiano.Models;
    using VirtualPiano.Web.Infrastructure.Mapping;

    public class AllCategoriesViewModel : IMapFrom<AdCategory>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<OpenningBasicInfoViewModel> Opennings { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<AdCategory, AllCategoriesViewModel>()
                .ForMember(dest => dest.Opennings, opt => opt.MapFrom(category => category.MusicAds
                    .AsQueryable()
                    .Where(post => !post.IsDeleted)
                    .OrderBy(ad => ad.Title)
                    .Select(ad => new OpenningBasicInfoViewModel()
                    {
                        Id = ad.Id,
                        Title = ad.Title
                    })));
        }
    }
}