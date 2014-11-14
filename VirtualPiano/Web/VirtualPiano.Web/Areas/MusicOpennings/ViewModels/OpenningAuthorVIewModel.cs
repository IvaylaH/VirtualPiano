namespace VirtualPiano.Web.Areas.MusicOpennings.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using VirtualPiano.Models;
    using VirtualPiano.Web.Infrastructure.Mapping;

    public class OpenningAuthorVIewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string Username { get; set; }
    }
}