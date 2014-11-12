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

        public ActionResult Details(int? id)
        {
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
    }
}