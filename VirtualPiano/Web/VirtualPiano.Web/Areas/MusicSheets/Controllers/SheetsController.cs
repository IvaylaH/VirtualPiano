﻿namespace VirtualPiano.Web.Areas.MusicSheets.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;

    using VirtualPiano.Data;
    using VirtualPiano.Models;
    using VirtualPiano.Web.Areas.MusicSheets.InputModels;
    using VirtualPiano.Web.Areas.MusicSheets.ViewModels;
    using VirtualPiano.Web.Controllers;

    public class SheetsController : BaseController
    {
        private const int SheetsPerPage = 5;
        private const int DefaultPage = 1;

        public SheetsController(IVirtualPianoData data)
            : base(data)
        {
        }

        [Authorize]
        [HttpGet]
        public ActionResult Upload()
        {
            var categories = this.Data.MusicSheetsCategories.All().ToList();
            ViewData["categories"] = categories;

            var model = new InputMusicSheetViewModel();
            return View(model);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Upload(InputMusicSheetViewModel inputModel)
        {
            var categories = this.Data.MusicSheetsCategories.All().ToList();
            ViewData["categories"] = categories;

            var selectedCategoryName = this.Request.Params["listCategories"];
            ViewData["categoryError"] = selectedCategoryName == "Select Category" ? "* Song Genre is a required field." : null;

            if (selectedCategoryName == "Select Category" || !ModelState.IsValid)
            {
                return View(inputModel);
            }

            var category =  this.Data.MusicSheetsCategories.All().Where(c => c.Name == selectedCategoryName).FirstOrDefault();

            var musicSheetId = 0;
            if (this.Data.Artists.All().Any(a => a.Name == inputModel.Artist))
            {
                var artist = this.Data.Artists.All().FirstOrDefault(a => a.Name == inputModel.Artist);

                musicSheetId = this.CreateMusicSheetObject(artist, inputModel, category);
            }
            else
            {
                var artist = new Artist
                {
                    Name = inputModel.Artist
                };

                this.Data.Artists.Add(artist);

                musicSheetId = this.CreateMusicSheetObject(artist, inputModel, category);
            }

            return this.RedirectToAction("Details", new { id = musicSheetId });
        }

        public ActionResult All(string sortBy, int page = DefaultPage, int perPage = SheetsPerPage)
        {
            var pagesCount = (int)Math.Ceiling(this.Data.MusicSheets.All().Count() / (decimal)perPage);
            var musicSheets = this.Data.MusicSheets.All();
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

            var musicSheet = this.Data.MusicSheets.GetById(id);
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

        private int CreateMusicSheetObject(Artist artist, InputMusicSheetViewModel inputModel, MusicSheetsCategory category)
        {
            var musicSheet = new MusicSheet
            {
                Title = inputModel.Title,
                ArtistId = artist.Id,
                CategoryId = category.Id,
                Notes = inputModel.Notes,
                CreatedOn = DateTime.Now
            };

            artist.MusicSheets.Add(musicSheet);
            category.MusicSheets.Add(musicSheet);
            this.Data.MusicSheets.Add(musicSheet);
            this.Data.SaveChanges();

            return musicSheet.Id;
        }
    }
}