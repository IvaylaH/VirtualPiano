namespace VirtualPiano.Web.Areas.Administration.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using VirtualPiano.Common;
    using VirtualPiano.Data;
    using VirtualPiano.Models;
    using VirtualPiano.Web.Areas.Administration.Controllers.Base;
    using VirtualPiano.Web.Areas.Administration.ViewModels.Users;

    public class UsersController : AdminController
    {
        public UsersController(IVirtualPianoData data)
            : base(data)
        {
        }

        // GET: Administration/Users
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var users = this.Data.Users.All()
                .Where(u => u.UserName != "admin@vp.com")
                .Project()
                .To<UsersViewModel>()
                .ToDataSourceResult(request);


            return Json(users);
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, UsersViewModel inputModel)
        {
            if (inputModel != null && ModelState.IsValid)
            {
                var dbModel = this.Data.Users.GetById(inputModel.Id);

                dbModel.UserName = inputModel.UserName;
                dbModel.FirstName = inputModel.FirstName;
                dbModel.LastName = inputModel.LastName;
                dbModel.ModifiedOn = DateTime.Now;

                this.Data.SaveChanges();
            }

            return Json(new[] { inputModel }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, UsersViewModel inputModel)
        {
            if (inputModel != null && ModelState.IsValid)
            {
                var dbModel = this.Data.Users.All()
                    .Include(u => u.Ads)
                    .FirstOrDefault(u => u.Id == inputModel.Id);

                // TODO: Implement logic for deleting user and changing the opennings view models
                if (dbModel.Ads.Count == 0)
                {
                    this.Data.Users.Delete(dbModel);
                    this.Data.SaveChanges();
                }
            }

            return Json(new[] { inputModel }.ToDataSourceResult(request, ModelState));
        }
    }
}