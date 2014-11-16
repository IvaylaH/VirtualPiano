namespace VirtualPiano.Web.ViewModels.Home
{
    using VirtualPiano.Models;
    using VirtualPiano.Web.Infrastructure.Mapping;

    public class UserBasicInfoViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }
    }
}