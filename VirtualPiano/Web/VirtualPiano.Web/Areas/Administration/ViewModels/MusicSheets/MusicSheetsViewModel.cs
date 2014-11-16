namespace VirtualPiano.Web.Areas.Administration.ViewModels.MusicSheets
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using VirtualPiano.Models;
    using VirtualPiano.Web.Infrastructure.Mapping;

    public class MusicSheetsViewModel : IMapFrom<MusicSheet>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 100)]
        public string Title { get; set; }

        [Required]
        public string Notes { get; set; }

        [Required]
        public string CategoryName { get; set; }

        [Required]
        public string ArtistName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<MusicSheet, MusicSheetsViewModel>()
                .ForMember(dest => dest.ArtistName, opt => opt.MapFrom(sheet => sheet.Artist.Name))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(sheet => sheet.Category.Name))
                .ReverseMap();
        }
    }
}