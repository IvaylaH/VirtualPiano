using System.Web.Mvc;

namespace VirtualPiano.Web.Areas.MusicOpennings
{
    public class MusicOpenningsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MusicOpennings";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MusicOpennings_default",
                "MusicOpennings/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}