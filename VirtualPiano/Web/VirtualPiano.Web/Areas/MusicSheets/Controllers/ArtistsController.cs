namespace VirtualPiano.Web.Areas.MusicSheets.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using VirtualPiano.Data;
    using VirtualPiano.Models;
    using VirtualPiano.Web.Areas.MusicSheets.ViewModels;
    using VirtualPiano.Web.Controllers;

    public class ArtistsController : BaseController
    {
        private const int artistsPerPage = 10;
        private const int DefaultPage = 1;

        public ArtistsController(IVirtualPianoData data)
            : base (data)
        {
        }

        public ActionResult Details(int id)
        {
            //var artist = this.artistsRepo.GetById(id)
            //    .Project().To<>
            return null;
        }

        public ActionResult All(string sortBy, int page = DefaultPage, int perPage = artistsPerPage)
        {
            var pagesCount = (int)Math.Ceiling(this.Data.Artists.All().Count() / (decimal)perPage);
            ViewData["sortBy"] = sortBy;
            ViewData["page"] = page;

            var artists = this.Data.Artists.All().Include(a => a.MusicSheets);
            artists = this.SortArtists(sortBy, artists);

            var artistsToDisplay = artists
                .Skip(perPage * (page - 1))
                .Take(perPage)
                .ToList();

            var categories = this.Data.MusicSheetsCategories.All().Include(a => a.MusicSheets).ToList();

            Mapper.CreateMap<Artist, AllArtistsViewModel>();
            Mapper.CreateMap<ICollection<MusicSheet>, ICollection<ArtistsCollectionOfSheetViewModel>>();

            var artistsModel = Mapper.Map<ICollection<Artist>, ICollection<AllArtistsViewModel>>(artistsToDisplay);

            var model = new AllArtistsPageViewModel()
            {
                Artists = artistsModel,
                CurrentPage = page,
                PagesCount = pagesCount
            };

            return View(model);
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