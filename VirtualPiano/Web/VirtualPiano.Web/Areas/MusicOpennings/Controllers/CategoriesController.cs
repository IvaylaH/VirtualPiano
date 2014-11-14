namespace VirtualPiano.Web.Areas.MusicOpennings.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using VirtualPiano.Data;
    using VirtualPiano.Web.Areas.MusicOpennings.ViewModels;
    using VirtualPiano.Web.Controllers;

    public class CategoriesController : BaseController
    {
        public CategoriesController(IVirtualPianoData data)
            : base(data)
        {
        }

        public ActionResult All()
        {
            var categories = this.Data.AdCategories.All()
                .OrderBy(category => category.Name)
                .Project()
                .To<OpenningCategoryViewModel>()
                .ToList();

            return View(categories);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("All");
            }

            var category = this.Data.AdCategories.All()
                .Project()
                .To<AllCategoriesViewModel>()
                .FirstOrDefault(ad => ad.Id == id);

            if (category == null)
            {
                return this.HttpNotFound("The category was not found");
            }

            return View(category);
        }
    }
}