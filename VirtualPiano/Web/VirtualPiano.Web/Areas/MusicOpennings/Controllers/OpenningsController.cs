namespace VirtualPiano.Web.Areas.MusicOpennings.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using VirtualPiano.Common;
    using VirtualPiano.Data;
    using VirtualPiano.Models;
    using VirtualPiano.Web.Areas.MusicOpennings.ViewModels;
    using VirtualPiano.Web.Controllers;

    public class OpenningsController : BaseController
    {
        public OpenningsController(IVirtualPianoData data)
            : base(data)
        {
        }

        public ActionResult All(string sortBy, int page = GlobalConstants.DefaultPageForViews, int perPage = GlobalConstants.DefaultItemsPerPageForCustomViews)
        {
            var pagesCount = (int)Math.Ceiling(this.Data.MusicAds.All().Count() / (decimal)perPage);
            var musicAds = this.Data.MusicAds.All();
            ViewData["sortBy"] = sortBy;
            ViewData["page"] = page;

            musicAds = this.SortMusicAds(sortBy, musicAds);

            var mappedAds = musicAds
                .Skip(perPage * (page - 1))
                .Take(perPage)
                .Project()
                .To<AllAdsViewModel>()
                .ToList();

            var adsPage = new AllAdsPageViewModel()
            {
                Opennings = mappedAds,
                PagesCount = pagesCount,
                CurrentPage = page
            };

            return View(adsPage);
        }

        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("All");
            }

            var openning = this.Data.MusicAds.All()
                .Project()
                .To<AdDetailsViewModel>()
                .FirstOrDefault(ad => ad.Id == id);

            if (openning == null)
            {
                return this.HttpNotFound("The openning was not found");
            }

            return View(openning);
        }

        private IQueryable<MusicAd> SortMusicAds(string sortBy, IQueryable<MusicAd> musicAds)
        {
            var sortParam = sortBy == null ? "" : sortBy;
            var sortByToLowerCase = sortParam.ToLowerInvariant();
            ViewBag.TitleSortParam = (!String.IsNullOrEmpty(sortByToLowerCase) && sortByToLowerCase == "title") ? "title_desc" : "Title";
            ViewBag.ArtistSortParam = (!String.IsNullOrEmpty(sortByToLowerCase) && sortByToLowerCase == "artist") ? "artist_desc" : "Artist";
            ViewBag.CategorySortParam = (!String.IsNullOrEmpty(sortByToLowerCase) && sortByToLowerCase == "category") ? "category_desc" : "Category";
            ViewBag.DateSortParam = (!String.IsNullOrEmpty(sortByToLowerCase) && sortByToLowerCase == "date") ? "date_desc" : "Date";

            switch (sortBy)
            {
                case "title_desc":
                    return this.SortByTitleDescending(musicAds);
                case "Title":
                    return this.SortByTitleAscending(musicAds);
                case "artist_desc":
                    return this.SortByArtistDescending(musicAds);
                case "Artist":
                    return this.SortByArtistAscending(musicAds);
                case "category_desc":
                    return this.SortByCategoryDescending(musicAds);
                case "Category":
                    return this.SortByCategoryAscending(musicAds);
                case "Date":
                    return this.SortByDateDescending(musicAds);
                default:
                    return musicAds.OrderBy(ad => ad.CreatedOn);
            };
        }

        private IQueryable<MusicAd> SortByTitleAscending(IQueryable<MusicAd> musicAds)
        {
            return musicAds.OrderBy(ad => ad.Title);
        }

        private IQueryable<MusicAd> SortByTitleDescending(IQueryable<MusicAd> musicAds)
        {
            return musicAds.OrderByDescending(ad => ad.Title);
        }

        private IQueryable<MusicAd> SortByArtistAscending(IQueryable<MusicAd> musicAds)
        {
            return musicAds.OrderBy(ad => ad.Author.UserName);
        }

        private IQueryable<MusicAd> SortByArtistDescending(IQueryable<MusicAd> musicAds)
        {
            return musicAds.OrderByDescending(ad => ad.Author.UserName);
        }

        private IQueryable<MusicAd> SortByCategoryAscending(IQueryable<MusicAd> musicAds)
        {
            return musicAds.OrderBy(ad => ad.Category.Name);
        }

        private IQueryable<MusicAd> SortByCategoryDescending(IQueryable<MusicAd> musicAds)
        {
            return musicAds.OrderByDescending(ad => ad.Category.Name);
        }

        private IQueryable<MusicAd> SortByDateDescending(IQueryable<MusicAd> musicAds)
        {
            return musicAds.OrderByDescending(ad => ad.CreatedOn);
        }
    }
}