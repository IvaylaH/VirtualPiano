namespace VirtualPiano.Web.Areas.MusicSheets.Controllers.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

using VirtualPiano.Data.Common.Repository;

    public abstract class BaseController : Controller
    {
        //private readonly IDeletableEntityRepository<T> repo;
        //public IQueryable<T> Sort(string sortBy, IQueryable<T> sortableEntity)
        //{
        //    var sortParam = sortBy == null ? "" : sortBy;
        //    var sortByToLowerCase = sortParam.ToLowerInvariant();
        //    ViewBag.TitleSortParam = (!String.IsNullOrEmpty(sortByToLowerCase) && sortByToLowerCase == "title") ? "title_desc" : "Title";
        //}

        //public IQueryable<T> SortByNameAscending(IQueryable<T> sortableEntity)
        //{
        //    throw new NotImplementedException();
        //}

        //public IQueryable<T> SortByNameDescending(IQueryable<T> sortableEntity)
        //{
        //    throw new NotImplementedException();
        //}
    }
}