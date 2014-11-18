namespace VirtualPiano.Web.Areas.Administration.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Microsoft.AspNet.Identity;

    using VirtualPiano.Data;
    using VirtualPiano.Models;
    using VirtualPiano.Web.Areas.Administration.Controllers.Base;
    using VirtualPiano.Web.Areas.Administration.ViewModels.OpenningsCategories;

    public class OpenningsCategoriesController : AdminController
    {
        public OpenningsCategoriesController(IVirtualPianoData data)
            : base(data)
        {
        }
        // GET: Administration/OpenningsCategories
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var adCategories = this.Data.AdCategories.All()
                .Project()
                .To<OpenningCategoriesViewModel>()
                .ToDataSourceResult(request);

            return Json(adCategories);
        }

        [HttpPost]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, OpenningCategoriesViewModel inputModel)
        {
            if (inputModel != null && ModelState.IsValid)
            {
                var dbModel = new AdCategory()
                {
                    Name = inputModel.Name,
                    CreatedOn = DateTime.Now
                };

                this.Data.AdCategories.Add(dbModel);
                this.Data.SaveChanges();

                inputModel.Id = dbModel.Id;
            }

            return Json(new[] { inputModel }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, OpenningCategoriesViewModel  inputModel)
        {
            if (inputModel != null && ModelState.IsValid)
            {
                var dbModel = this.Data.AdCategories.GetById(inputModel.Id);

                dbModel.Name = inputModel.Name;
                dbModel.ModifiedOn = DateTime.Now;

                this.Data.SaveChanges();
            }

            return Json(new[] { inputModel }.ToDataSourceResult(request, ModelState));
        }
    }
}