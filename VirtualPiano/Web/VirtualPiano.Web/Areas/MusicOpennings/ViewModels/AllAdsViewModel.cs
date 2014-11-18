namespace VirtualPiano.Web.Areas.MusicOpennings.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Web;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using VirtualPiano.Models;
    using VirtualPiano.Web.Infrastructure.Mapping;

    public class AllAdsViewModel : IMapFrom<MusicAd>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CategoryName { get; set; }

        [DefaultValue("Anonymous")]
        public string AuthorName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<MusicAd, AllAdsViewModel>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(ad => ad.Author.UserName))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(ad => ad.Category.Name))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(ad => ad.CreatedOn));
        }
    }
}