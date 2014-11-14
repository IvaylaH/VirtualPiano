namespace VirtualPiano.Web.Areas.MusicSheets.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    using AutoMapper;

    using VirtualPiano.Models;
    using VirtualPiano.Web.Infrastructure.Mapping;

    public class CollectionOfSheetViewModel : IMapFrom<MusicSheet>
    {
        public int Id { get; set; }

        [Display(Name = "Song Title")]
        public string Title { get; set; }
    }
}