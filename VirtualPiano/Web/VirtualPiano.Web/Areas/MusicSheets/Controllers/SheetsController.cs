namespace VirtualPiano.Web.Areas.MusicSheets.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;

    using VirtualPiano.Data.Common.Repository;
    using VirtualPiano.Models;
    using VirtualPiano.Web.Areas.MusicSheets.ViewModels;

    public class SheetsController : Controller
    {
        private readonly IDeletableEntityRepository<MusicSheet> repo;

        public SheetsController(IDeletableEntityRepository<MusicSheet> sheetsRepo)
        {
            this.repo = sheetsRepo;
        }

        // GET: MusicSheets/Sheets
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult All(string sortBy)
        {
            var musicSheets = this.repo.All(); 
            musicSheets = this.SortMusicSheets(sortBy, musicSheets);

            Mapper.CreateMap<MusicSheet, MusicSheetsListAllModelView>();

            var mappedSheets = Mapper.Map<ICollection<MusicSheet>, ICollection<MusicSheetsListAllModelView>>(musicSheets.ToList());

            return View(mappedSheets);
//            return null;
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("All");
            }

            var musicSheet = this.repo.GetById(id);
            Mapper.CreateMap<MusicSheet, MusicSheetDetailsViewModel>()
                .ForMember(dest => dest.ArtistName, opt => opt.MapFrom(src => src.Artist.Name))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes.Split(new string[] { "/r" }, StringSplitOptions.RemoveEmptyEntries)));

            var mappedMusicSheet = Mapper.Map<MusicSheet, MusicSheetDetailsViewModel>(musicSheet);

            if (mappedMusicSheet == null)
            {
                return this.HttpNotFound("There is no such music sheet in DB");
            }

            return View(mappedMusicSheet);
        }

        private IQueryable<MusicSheet> SortByTitleAscending(IQueryable<MusicSheet> musicSheets)
        {
            return musicSheets.OrderBy(sheet => sheet.Title);
        }

        private IQueryable<MusicSheet> SortByTitleDescending(IQueryable<MusicSheet> musicSheets)
        {
            return musicSheets.OrderByDescending(sheet => sheet.Title);
        }

        private IQueryable<MusicSheet> SortByArtistAscending(IQueryable<MusicSheet> musicSheets)
        {
            return musicSheets.OrderBy(sheet => sheet.Artist.Name);
        }

        private IQueryable<MusicSheet> SortByArtistDescending(IQueryable<MusicSheet> musicSheets)
        {
            return musicSheets.OrderByDescending(sheet => sheet.Artist.Name);
        }

        private IQueryable<MusicSheet> SortByCategoryAscending(IQueryable<MusicSheet> musicSheets)
        {
            return musicSheets.OrderBy(sheet => sheet.Category.Name);
        }

        private IQueryable<MusicSheet> SortByCategoryDescending(IQueryable<MusicSheet> musicSheets)
        {
            return musicSheets.OrderByDescending(sheet => sheet.Category.Name);
        }

        private IQueryable<MusicSheet> SortMusicSheets(string sortBy, IQueryable<MusicSheet> musicSheets)
        {
            var sortParam = sortBy == null ? "" : sortBy;
            var sortByToLowerCase = sortParam.ToLowerInvariant();
            ViewBag.TitleSortParam = (!String.IsNullOrEmpty(sortByToLowerCase) && sortByToLowerCase == "title") ? "title_desc" : "Title";
            ViewBag.ArtistSortParam = (!String.IsNullOrEmpty(sortByToLowerCase) && sortByToLowerCase == "artist") ? "artist_desc" : "Artist";
            ViewBag.CategorySortParam = (!String.IsNullOrEmpty(sortByToLowerCase) && sortByToLowerCase == "category") ? "category_desc" : "Category";

            switch (sortBy)
            {
                case "title_desc":
                    musicSheets = this.SortByTitleDescending(musicSheets);
                    break;
                case "Title":
                    musicSheets = this.SortByTitleAscending(musicSheets);
                    break;
                case "artist_desc":
                    musicSheets = this.SortByArtistDescending(musicSheets);
                    break;
                case "Artist":
                    musicSheets = this.SortByArtistAscending(musicSheets);
                    break;
                case "category_desc":
                    musicSheets = this.SortByCategoryDescending(musicSheets);
                    break;
                case "Category":
                    musicSheets = this.SortByCategoryAscending(musicSheets);
                    break;
                default:
                    musicSheets = musicSheets.OrderBy(sheet => sheet.Id);
                    break;
            }

            return musicSheets;
        }
    }
}