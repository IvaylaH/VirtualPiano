namespace VirtualPiano.Web.Areas.MusicSheets.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using VirtualPiano.Data;
    using VirtualPiano.Web.Areas.MusicSheets.ViewModels;
    using VirtualPiano.Web.Controllers;

    public class CategoriesController : BaseController
    {
        public CategoriesController(IVirtualPianoData data)
            : base(data)
        {
        }

        public ActionResult All()
        {
            var categories = this.Data.MusicSheetsCategories.All()
                .OrderBy(category => category.Name)
                .Project()
                .To<CategoriesViewModel>()
                .ToList();

            return View(categories);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("All");
            }

            var category = this.Data.MusicSheetsCategories.All()
                .Project()
                .To<CategoriesViewModel>()
                .FirstOrDefault(cat => cat.Id == id);

            if (category == null)
            {
                return this.HttpNotFound("The category was not found");
            }

            return View(category);
        }
    }
}