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
    using VirtualPiano.Web.Areas.Administration.ViewModels.MusicOpennings;

    public class MusicOpenningsController : AdminController
    {
        public MusicOpenningsController(IVirtualPianoData data)
            : base(data)
        {
        }

        // GET: Administration/MusicAds
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var musicSheets = this.Data.MusicAds.All()
                .Project()
                .To<MusicAdsViewModel>()
                .ToDataSourceResult(request);

            return Json(musicSheets);
        }

        [HttpPost]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, MusicAdsViewModel inputModel)
        {
            if (inputModel != null && ModelState.IsValid)
            {
                var category = this.GetCategory(inputModel.CategoryName);

                var authorId = this.User.Identity.GetUserId();

                var dbModel = new MusicAd()
                {
                    Title = inputModel.Title,
                    Content = inputModel.Content,
                    CategoryId = category.Id,
                    AuthorId = authorId,
                    CreatedOn = DateTime.Now
                };

                this.Data.MusicAds.Add(dbModel);
                var author = this.GetAuthor(authorId);

                author.Ads.Add(dbModel);
                category.MusicAds.Add(dbModel);
                this.Data.SaveChanges();

                inputModel.Id = dbModel.Id;
            }

            return Json(new[] { inputModel }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, MusicAdsViewModel inputModel)
        {
            if (inputModel != null && ModelState.IsValid)
            {
                var dbModel = this.Data.MusicAds.GetById(inputModel.Id);
                var category = this.GetCategory(inputModel.CategoryName);

                if (dbModel.CategoryId != category.Id)
                {
                    category.MusicAds.Add(dbModel);
                    dbModel.CategoryId = category.Id;
                }

                dbModel.Title = inputModel.Title;
                dbModel.Content = inputModel.Content;
                dbModel.ModifiedOn = DateTime.Now;

                this.Data.SaveChanges();
            }

            return Json(new[] { inputModel }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, MusicAdsViewModel inputModel)
        {
            if (inputModel != null && ModelState.IsValid)
            {
                var dbModel = this.Data.MusicAds.All()
                    .FirstOrDefault(ad => ad.Id == inputModel.Id);

                this.Data.MusicAds.Delete(dbModel);
                this.Data.SaveChanges();
            }

            return Json(new[] { inputModel }.ToDataSourceResult(request, ModelState));
        }

        private ApplicationUser GetAuthor(string artistId)
        {
            return this.Data.Users.All()
                .Include(a => a.Ads)
                    .FirstOrDefault(u => u.Id == artistId);
        }

        private AdCategory GetCategory(string categoryName)
        {
            return this.Data.AdCategories.All()
                .Include(c => c.MusicAds)
                    .FirstOrDefault(c => c.Name == categoryName);
        }
    }
}