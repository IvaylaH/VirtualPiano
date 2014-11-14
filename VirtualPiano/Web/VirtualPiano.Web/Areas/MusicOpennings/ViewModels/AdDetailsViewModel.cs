namespace VirtualPiano.Web.Areas.MusicOpennings.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using VirtualPiano.Models;
    using VirtualPiano.Web.Infrastructure.Mapping;

    public class AdDetailsViewModel : IMapFrom<MusicAd>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public OpenningCategoryViewModel Category { get; set; }

        public OpenningAuthorVIewModel Author { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<MusicAd, AdDetailsViewModel>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(ad => ad.Author))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(ad => ad.Category))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(ad => ad.CreatedOn));
        }
    }
}