namespace VirtualPiano.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using VirtualPiano.Data;
    using VirtualPiano.Data.Common.Repository;
    using VirtualPiano.Models;
    public class HomeController : Controller
    {
        private IRepository<MusicSheet> musicSheetsRepo;

        public HomeController()
            : this(new GenericRepository<MusicSheet>(new ApplicationDbContext()))
        {
        }

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
            var musicSheets = this.musicSheetsRepo.All();

            return View(musicSheets);
        }
    }
}