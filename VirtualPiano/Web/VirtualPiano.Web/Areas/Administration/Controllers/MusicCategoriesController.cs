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
    using VirtualPiano.Web.Areas.Administration.ViewModels.MusicCategories;

    public class MusicCategoriesController : AdminController
    {
        public MusicCategoriesController(IVirtualPianoData data)
            : base(data)
        {
        }

        // GET: Administration/Artists
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var musicCategories = this.Data.MusicSheetsCategories.All()
                .Project()
                .To<MusicCategoriesViewModel>()
                .ToDataSourceResult(request);

            return Json(musicCategories);
        }

        [HttpPost]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, MusicCategoriesViewModel inputModel)
        {
            if (inputModel != null && ModelState.IsValid)
            {
                var dbModel = new MusicSheetsCategory()
                {
                    Name = inputModel.Name,
                    CreatedOn = DateTime.Now
                };

                this.Data.MusicSheetsCategories.Add(dbModel);
                this.Data.SaveChanges();

                inputModel.Id = dbModel.Id;
            }

            return Json(new[] { inputModel }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, MusicCategoriesViewModel inputModel)
        {
            if (inputModel != null && ModelState.IsValid)
            {
                var dbModel = this.Data.MusicSheetsCategories.GetById(inputModel.Id);

                dbModel.Name = inputModel.Name;
                dbModel.ModifiedOn = DateTime.Now;

                this.Data.SaveChanges();
            }

            return Json(new[] { inputModel }.ToDataSourceResult(request, ModelState));
        }
    }
}