namespace VirtualPiano.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using VirtualPiano.Data;
    using VirtualPiano.Models;
    using VirtualPiano.Web.Areas.Administration.Controllers.Base;
    using VirtualPiano.Web.Areas.Administration.ViewModels.MusicSheets;

    public class MusicSheetsController : AdminController
    {
        public MusicSheetsController(IVirtualPianoData data)
            : base(data)
        {
        }

        // GET: Administration/MusicSheets
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var musicSheets = this.Data.MusicSheets.All()
                .Project()
                .To<MusicSheetsViewModel>()
                .ToDataSourceResult(request);

            return Json(musicSheets);
        }

        [HttpPost]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, MusicSheetsViewModel inputModel)
        {
            if (inputModel != null && ModelState.IsValid)
            {
                var category = this.GetCategory(inputModel.CategoryName);

                var artist = this.GetArtist(inputModel.ArtistName);

                // TODO: validation for category

                if (artist == null)
                {
                    artist = this.CreateNewArtist(inputModel.ArtistName);
                }

                var dbModel = new MusicSheet()
                {
                    Title = inputModel.Title,
                    Notes = inputModel.Notes,
                    CategoryId = category.Id,
                    ArtistId = artist.Id,
                    CreatedOn = DateTime.Now
                };

                this.Data.MusicSheets.Add(dbModel);
                artist.MusicSheets.Add(dbModel);
                category.MusicSheets.Add(dbModel);
                this.Data.SaveChanges();

                inputModel.Id = dbModel.Id;
            }

            return Json(new[] { inputModel }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, MusicSheetsViewModel inputModel)
        {
            if (inputModel != null && ModelState.IsValid)
            {
                var dbModel = this.Data.MusicSheets.GetById(inputModel.Id);
                var category = this.GetCategory(inputModel.CategoryName);
                var artist = this.GetArtist(inputModel.ArtistName);

                if (artist == null)
                {
                    artist = this.CreateNewArtist(inputModel.ArtistName);
                }

                if (dbModel.CategoryId != category.Id)
                {
                    category.MusicSheets.Add(dbModel);
                    dbModel.CategoryId = category.Id;

                }

                if (dbModel.ArtistId != artist.Id)
                {
                    artist.MusicSheets.Add(dbModel);
                    dbModel.ArtistId = artist.Id;
                }

                dbModel.Title = inputModel.Title;
                dbModel.Notes = inputModel.Notes;
                dbModel.ModifiedOn = DateTime.Now;

                this.Data.SaveChanges();

            }

            return Json(new[] { inputModel }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, MusicSheetsViewModel inputModel)
        {
            //if (inputModel != null && ModelState.IsValid)
            //{
            //    var dbModel = this.Data.MusicSheets.GetById(inputModel.Id);
            //    var category = this.GetCategory(inputModel.CategoryName);
            //    var artist = this.GetArtist(inputModel.ArtistName);

            //    category.MusicSheets.Remove(dbModel);
            //    artist.MusicSheets.Remove(dbModel);

            //    this.Data.MusicSheets.Delete(dbModel);
            //    this.Data.SaveChanges();
            //}

            //return Json(new[] { inputModel }.ToDataSourceResult(request, ModelState));
            return null;
        }

        private Artist GetArtist(string artistName)
        {
            return this.Data.Artists.All()
                    .FirstOrDefault(a => a.Name == artistName);
        }

        private MusicSheetsCategory GetCategory(string categoryName)
        {
            return this.Data.MusicSheetsCategories.All()
                                .FirstOrDefault(c => c.Name == categoryName);
        }

        private Artist CreateNewArtist(string artistName)
        {
            var artist = new Artist()
            {
                Name = artistName
            };

            this.Data.Artists.Add(artist);
            this.Data.SaveChanges();

            return artist;
        }
    }
}