using MyWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWebApp.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Library(string name)
        {
            AnimeAccount animeAccount = db.AnimeAccounts.Where(a => a.UserName == name).SingleOrDefault();
            if (animeAccount == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Title = animeAccount.UserName + "'s library";
            ViewBag.UserName = animeAccount.UserName;

            List<LibraryListing> accountListings = new List<LibraryListing>();
            accountListings = db.LibraryListings.Where(l => l.AnimeAccountId == animeAccount.Id).ToList();
            return View(accountListings);
        }
    }
}