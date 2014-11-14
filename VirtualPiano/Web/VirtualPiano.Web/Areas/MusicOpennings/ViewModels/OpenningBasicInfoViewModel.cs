namespace VirtualPiano.Web.Areas.MusicOpennings.ViewModels
{
    using System;

    using VirtualPiano.Models;
    using VirtualPiano.Web.Infrastructure.Mapping;

    public class OpenningBasicInfoViewModel : IMapFrom<MusicAd>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}