namespace VirtualPiano.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using VirtualPiano.Data;
    using VirtualPiano.Data.Common.Repository;
    using VirtualPiano.Models;
    using VirtualPiano.Web.ViewModels.Home;

    public class HomeController : Controller
    {
        private IRepository<MusicSheet> musicSheetsRepo;

        public HomeController(IRepository<MusicSheet> sheetsRepo)
        {
            this.musicSheetsRepo = sheetsRepo;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MusicSheets()
        {
            var musicSheets = this.musicSheetsRepo.All().Project().To<MusicSheetsViewModel>();

            return View(musicSheets);
        }
    }
}