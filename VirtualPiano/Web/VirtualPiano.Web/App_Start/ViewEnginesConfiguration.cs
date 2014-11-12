namespace VirtualPiano.Web.App_Start
{
    using System.Web.Mvc;

    public class ViewEnginesConfiguration
    {
        public static void RegisterViewEngines(ViewEngineCollection viewEngines)
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
        }
    }
}