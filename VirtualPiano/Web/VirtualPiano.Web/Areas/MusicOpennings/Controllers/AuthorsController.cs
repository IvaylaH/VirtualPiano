namespace VirtualPiano.Web.Areas.MusicOpennings.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using VirtualPiano.Common;
    using VirtualPiano.Data;
    using VirtualPiano.Models;
    using VirtualPiano.Web.Controllers;
    using VirtualPiano.Web.Areas.MusicOpennings.ViewModels;

    public class AuthorsController : BaseController
    {
        public AuthorsController(IVirtualPianoData data)
            : base(data)
        {
        }

        public ActionResult AuthorOpennings(string id, string sortBy, int page = GlobalConstants.DefaultPageForViews, int perPage = GlobalConstants.DefaultItemsPerPageForCustomViews)
        {
            if (id == null)
            {
                this.RedirectToAction("Details", "Authors");
            }

            ViewData["guid"] = id;
            var author = this.Data.Users.All()
                .Project()
                .To<AuthorDetailsViewModel>()
                .FirstOrDefault(a => a.Id == id);

            if (author == null)
            {
                return this.HttpNotFound("There is no such user");
            }

            var pagesCount = (int)Math.Ceiling(author.Ads.Count() / (decimal)perPage);
            var ads = this.SortMusicAds(sortBy, author.Ads)
                .Skip(perPage * (page - 1))
                .Take(perPage);

            author.Ads = ads;
            author.CurrentPage = page;
            author.PagesCount = pagesCount;

            return View(author);
        }

        private IEnumerable<AuthorsAdViewModel> SortMusicAds(string sortBy, IEnumerable<AuthorsAdViewModel> musicAds)
        {
            var sortParam = sortBy == null ? "" : sortBy;
            var sortByToLowerCase = sortParam.ToLowerInvariant();
            ViewBag.TitleSortParam = (!String.IsNullOrEmpty(sortByToLowerCase) && sortByToLowerCase == "title") ? "title_desc" : "Title";
            ViewBag.DateSortParam = (!String.IsNullOrEmpty(sortByToLowerCase) && sortByToLowerCase == "date") ? "date_desc" : "Date";

            switch (sortBy)
            {
                case "title_desc":
                    return this.SortByTitleDescending(musicAds);
                case "Title":
                    return this.SortByTitleAscending(musicAds);
                case "Date":
                    return this.SortByDateDescending(musicAds);
                default:
                    return musicAds.OrderBy(ad => ad.CreatedOn);
            };
        }

        private IEnumerable<AuthorsAdViewModel> SortByDateDescending(IEnumerable<AuthorsAdViewModel> musicAds)
        {
            return musicAds.OrderByDescending(ad => ad.CreatedOn);
        }


        private IEnumerable<AuthorsAdViewModel> SortByTitleAscending(IEnumerable<AuthorsAdViewModel> musicAds)
        {
            return musicAds.OrderBy(ad => ad.Title);
        }

        private IEnumerable<AuthorsAdViewModel> SortByTitleDescending(IEnumerable<AuthorsAdViewModel> musicAds)
        {
            return musicAds.OrderByDescending(ad => ad.Title);
        }
    }
}