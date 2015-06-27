using MyWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace MyWebApp.Controllers
{
    [Authorize]
    public class LibraryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpPost]
        public ActionResult UpdateProgress(LibraryListing model)
        {
            if (ModelState.IsValid)
            {
                var progress = model.Progress;
                var userId = User.Identity.GetUserId();
                var animeAccountId = db.AnimeAccounts.Where(c => c.ApplicationUserId == userId).First().Id;
                model.AnimeAccountId = animeAccountId;
                model = db.LibraryListings.Find(model.AnimeListId, model.AnimeAccountId);
                model.Progress = progress;
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();

                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public ActionResult AddToLibrary(LibraryListing model)
        {
            if (ModelState.IsValid)
            {
                LibraryListing libraryListing = model;
                var userId = User.Identity.GetUserId();
                var animeAccountId = db.AnimeAccounts.Where(c => c.ApplicationUserId == userId).First().Id;
                LibraryListing previousListing = db.LibraryListings.Find(model.AnimeListId, animeAccountId);
                if (previousListing != null)
                {
                    libraryListing.MyScore = previousListing.MyScore;
                    libraryListing.Progress = previousListing.Progress;
                    db.LibraryListings.Remove(previousListing);
                }
                libraryListing.AnimeAccountId = animeAccountId;
                db.LibraryListings.Add(libraryListing);
                db.SaveChanges();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public ActionResult RemoveFromLibrary(int id)
        {
            var userId = User.Identity.GetUserId();
            var animeAccountId = db.AnimeAccounts.Where(c => c.ApplicationUserId == userId).First().Id;
            LibraryListing previousListing = db.LibraryListings.Find(id, animeAccountId);
            if (previousListing != null)
            {
                db.LibraryListings.Remove(previousListing);
                db.SaveChanges();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            else
            {
                //Change maybe, throw a different notice if removing null in database
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
        }
    }
}