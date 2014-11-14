namespace VirtualPiano.Web.Areas.MusicOpennings.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using VirtualPiano.Models;
    using VirtualPiano.Web.Infrastructure.Mapping;

    public class OpenningCategoryViewModel : IMapFrom<AdCategory>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}