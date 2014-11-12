using System.Web.Mvc;

namespace VirtualPiano.Web.Areas.MusicSheets
{
    public class MusicSheetsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MusicSheets";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MusicSheets_default",
                "MusicSheets/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}