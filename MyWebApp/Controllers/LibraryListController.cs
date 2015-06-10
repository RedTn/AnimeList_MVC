using MyWebApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWebApp.Controllers
{
    [Authorize]
    public class LibraryListController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: LibraryList
        public ActionResult Index(int animeAccountId)
        {
            return View();
        }
    }
}