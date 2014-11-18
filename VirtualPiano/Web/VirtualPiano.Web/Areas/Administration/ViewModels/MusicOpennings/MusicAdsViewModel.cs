namespace VirtualPiano.Web.Areas.Administration.ViewModels.MusicOpennings
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using AutoMapper;

    using VirtualPiano.Models;
    using VirtualPiano.Web.Infrastructure.Mapping;

    public class MusicAdsViewModel : IMapFrom<MusicAd>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 150, MinimumLength = 10)]
        public string Title { get; set; }

        [Required]
        [StringLength(maximumLength: 1000, MinimumLength = 30)]
        public string Content { get; set; }

        public string AuthorName { get; set; }

        public string CategoryName { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<MusicAd, MusicAdsViewModel>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(ad => ad.Author.UserName))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(ad => ad.Category.Name))
                .ReverseMap();
        }
    }
}