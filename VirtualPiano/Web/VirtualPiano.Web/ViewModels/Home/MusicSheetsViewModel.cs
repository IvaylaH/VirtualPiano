using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using VirtualPiano.Models;
using VirtualPiano.Web.Infrastructure.Mapping;

namespace VirtualPiano.Web.ViewModels.Home
{
    public class MusicSheetsViewModel : IMapFrom<MusicSheet>
    {
        public string Title { get; set; }
    }
}