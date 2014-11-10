using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VirtualPiano.Web.Startup))]
namespace VirtualPiano.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
