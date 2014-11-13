namespace VirtualPiano.Web.Areas.MusicSheets.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using VirtualPiano.Data.Common.Repository;
    using VirtualPiano.Models;
    using VirtualPiano.Web.Areas.MusicSheets.ViewModels;

    public class ArtistsController : Controller
    {
        private const int artistsPerPage = 10;
        private const int DefaultPage = 1;

        private readonly IRepository<Artist> artistsRepo;

        public ArtistsController(IRepository<Artist> artists)
        {
            this.artistsRepo = artists;
        }

        public ActionResult All(string sortBy, int page = DefaultPage, int perPage = artistsPerPage)
        {
            var pagesCount = (int)Math.Ceiling(this.artistsRepo.All().Count() / (decimal)perPage);
            ViewData["sortBy"] = sortBy;
            ViewData["page"] = page;

            var artists = this.artistsRepo.All();
            artists = this.SortArtists(sortBy, artists);

            var artistsToDisplay = artists
                .Skip(perPage * (page - 1))
                .Take(perPage)
                .Project().To<AllArtistsViewModel>()
                .ToList();

            var model = new AllArtistsPageViewModel()
            {
                Artists = artistsToDisplay,
                CurrentPage = page,
                PagesCount = pagesCount
            };

            return View(model);
        }

        public ActionResult Details()
        {
            return null;
        }

        private IQueryable<Artist> SortArtists(string sortBy, IQueryable<Artist> artists)
        {
            var sortParam = sortBy == null ? "" : sortBy;
            var sortByToLowerCase = sortParam.ToLowerInvariant();
            ViewBag.NameSortParam = (!String.IsNullOrEmpty(sortByToLowerCase) && sortByToLowerCase == "name") ? "name_desc" : "Name";

            switch (sortBy)
            {
                case "name_desc":
                    return this.SortByNameDescending(artists);
                case "Name":
                    return this.SortByNameAscending(artists);
                default:
                    return artists.OrderBy(a => a.Id);
            }
        }

        private IQueryable<Artist> SortByNameAscending(IQueryable<Artist> artists)
        {
            return artists.OrderBy(a => a.Name);
        }

        private IQueryable<Artist> SortByNameDescending(IQueryable<Artist> artists)
        {
            return artists.OrderByDescending(a => a.Name);
        }
    }
}