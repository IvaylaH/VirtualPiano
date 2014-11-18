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
    using VirtualPiano.Web.Areas.Administration.ViewModels.Artists;

    public class ArtistsController : AdminController
    {
        public ArtistsController(IVirtualPianoData data)
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
            var musicSheets = this.Data.Artists.All()
                .Project()
                .To<ArtistsViewModel>()
                .ToDataSourceResult(request);

            return Json(musicSheets);
        }

        [HttpPost]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, ArtistsViewModel inputModel)
        {
            if (inputModel != null && ModelState.IsValid)
            {
                var dbModel = new Artist()
                {
                    Name = inputModel.Name,
                    CreatedOn = DateTime.Now
                };

                this.Data.Artists.Add(dbModel);
                this.Data.SaveChanges();

                inputModel.Id = dbModel.Id;
            }

            return Json(new[] { inputModel }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, ArtistsViewModel inputModel)
        {
            if (inputModel != null && ModelState.IsValid)
            {
                var dbModel = this.Data.Artists.GetById(inputModel.Id);

                dbModel.Name = inputModel.Name;
                dbModel.ModifiedOn = DateTime.Now;

                this.Data.SaveChanges();
            }

            return Json(new[] { inputModel }.ToDataSourceResult(request, ModelState));
        }
    }
}