namespace VirtualPiano.Web.Areas.MusicOpennings.ViewModels
{
    using System;

    using VirtualPiano.Models;
    using VirtualPiano.Web.Infrastructure.Mapping;

    public class AuthorsAdViewModel : IMapFrom<MusicAd>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}