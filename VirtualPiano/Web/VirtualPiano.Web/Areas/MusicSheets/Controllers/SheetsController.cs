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
    using VirtualPiano.Web.Areas.MusicSheets.InputModels;
    using VirtualPiano.Web.Areas.MusicSheets.ViewModels;

    public class SheetsController : Controller
    {
        private const int SheetsPerPage = 5;
        private const int DefaultPage = 1;

        private readonly IDeletableEntityRepository<MusicSheet> repo;
        private readonly IRepository<MusicSheetsCategory> categoriesRepo;
        private readonly IRepository<Artist> artistsRepo;

        public SheetsController(IDeletableEntityRepository<MusicSheet> sheetsRepo, IRepository<MusicSheetsCategory> musicCategories, IRepository<Artist> artists)
        {
            this.repo = sheetsRepo;
            this.categoriesRepo = musicCategories;
            this.artistsRepo = artists;
        }

        [Authorize]
        [HttpGet]
        public ActionResult Upload()
        {
            var categories = this.categoriesRepo.All().ToList();
            ViewData["categories"] = categories;

            var model = new InputMusicSheetViewModel();
            return View(model);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Upload(InputMusicSheetViewModel inputModel)
        {
            var categories = this.categoriesRepo.All().ToList();
            ViewData["categories"] = categories;

            var selectedCategoryName = this.Request.Params["listCategories"];
            ViewData["categoryError"] = selectedCategoryName == "Select Category" ? "* Song Genre is a required field." : null;
            
            if (selectedCategoryName == "Select Category" || !ModelState.IsValid)
            {
                return View(inputModel);
            }

            var categoryId = this.categoriesRepo.All().Where(c => c.Name == selectedCategoryName).Select(c => c.Id).FirstOrDefault();

            var artistId = 0;
            if (this.artistsRepo.All().Any(a => a.Name == inputModel.Artist))
            {
                artistId = this.artistsRepo.All().Where(a => a.Name == inputModel.Artist).Select(c => c.Id).FirstOrDefault();
            }
            else
            {
                var artist = new Artist
                {
                    Name = inputModel.Artist
                };

                this.artistsRepo.Add(artist);
                this.artistsRepo.SaveChanges();
                artistId = artist.Id;
            }

            var musicSheet = new MusicSheet
            {
                Title = inputModel.Title,
                ArtistId = artistId,
                CategoryId = categoryId,
                Notes = inputModel.Notes,
                CreatedOn = DateTime.Now
            };

            this.repo.Add(musicSheet);
            this.repo.SaveChanges();

            return this.RedirectToAction("Details", new { id = musicSheet.Id });
        }

        public ActionResult All(string sortBy, int page = DefaultPage, int perPage = SheetsPerPage)
        {
            var pagesCount = (int)Math.Ceiling(this.repo.All().Count() / (decimal)perPage);
            var musicSheets = this.repo.All();
            ViewData["sortBy"] = sortBy;
            ViewData["page"] = page;

            musicSheets = this.SortMusicSheets(sortBy, musicSheets);

            var result = musicSheets
                .Skip(perPage * (page - 1))
                .Take(perPage)
                .ToList();

            Mapper.CreateMap<MusicSheet, MusicSheetsListAllModelView>();

            var mappedSheets = Mapper.Map<ICollection<MusicSheet>, ICollection<MusicSheetsListAllModelView>>(result);

            var model = new SheetsAllViewModel()
            {
                MusicSheets = mappedSheets,
                CurrentPage = page,
                PagesCount = pagesCount
            };

            return View(model);
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
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)));

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
                    return this.SortByTitleDescending(musicSheets);
                case "Title":
                    return this.SortByTitleAscending(musicSheets);
                case "artist_desc":
                    return this.SortByArtistDescending(musicSheets);
                case "Artist":
                    return this.SortByArtistAscending(musicSheets);
                case "category_desc":
                    return this.SortByCategoryDescending(musicSheets);
                case "Category":
                    return this.SortByCategoryAscending(musicSheets);
                default:
                    return musicSheets.OrderBy(sheet => sheet.Id);
            };
        }
    }
}