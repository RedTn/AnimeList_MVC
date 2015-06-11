﻿using MyWebApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Net;

namespace MyWebApp.Controllers
{
    [Authorize]
    public class AnimeListController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //TODO: Find better way to do path literals
        private const string uploadDir = @"/Content/Images/AnimeList/";

        // GET: AnimeList
        public ActionResult Index()
        {
            return View();
        }

        // GET: AnimeList/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var userId = User.Identity.GetUserId();
            var animeAccountId = db.AnimeAccounts.Where(c => c.ApplicationUserId == userId).First().Id;
            LibraryListing previousListing = db.LibraryListings.Find(id, animeAccountId);
            if (previousListing != null)
            {
                ViewBag.AddHeader = previousListing.LibraryStatus;
            }
            else
            {
                ViewBag.AddHeader = "Add to Library";
            }

            ViewBag.DefaultAddHeader = "Add to Library";
            ViewBag.LibraryStatus = Enum.GetValues(typeof(LibraryStatus));
            ViewBag.RemoveHeader = "Remove from library";

            AnimeList animeList = (AnimeList)db.AnimeLists.Where(l => l.Id == id).SingleOrDefault();
            if (animeList != null)
                return View(animeList);
            else return View("ErrorModelNull");
        }

        [HttpPost]
        public ActionResult Details(AnimeList model)
        {
            AnimeList animeList = (AnimeList)db.AnimeLists.Where(l => l.Id == model.Id).SingleOrDefault();
            if (animeList != null)
            {
                db.AnimeLists.Remove(animeList);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else return View("ErrorModelNull");
        }

        // GET: AnimeList/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AnimeList/Create
        [HttpPost]
        public ActionResult Create(AnimeList animeList)
        {
            string returnString = "Entry successfully created!";

            if (ModelState.IsValid)
            {
                var listed = db.AnimeLists.Where(l => l.Title == animeList.Title && l.SeriesType == animeList.SeriesType).SingleOrDefault();
                if (listed != null)
                {
                    returnString = "There a series in the database with that title and series type! Entry overriden";
                    db.AnimeLists.Remove(listed);
                }

                if (animeList.ImageUpload != null)
                {
                    if (!AnimeList.validImageTypes.Contains(animeList.ImageUpload.ContentType))
                    {
                        return Content("Please choose an approriate filetype", "text/html");
                    }
                    else
                    {
                        var imagePath = Path.Combine(Server.MapPath(uploadDir), animeList.ImageUpload.FileName);
                        var imageUrl = Path.Combine(uploadDir, animeList.ImageUpload.FileName);

                        //TODO: Will need code to create directory if not exists, make sure method does not involve race conditions
                        //TODO: Security hole, can rewrite same imagepath with diffferent image

                        animeList.ImageUpload.SaveAs(imagePath);
                        animeList.ImageUrl = imageUrl;
                    }
                }

                db.AnimeLists.Add(animeList);
                db.SaveChanges();
            }
            else
            {
                returnString = "Error occured";
            }
            return Content(returnString, "text/html");
        }

        // GET: AnimeList/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AnimeList/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AnimeList/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AnimeList/Delete/5
        [HttpPost]
        public ActionResult Delete(AnimeList model)
        {
            AnimeList animeList = (AnimeList)db.AnimeLists.Where(l => l.Id == model.Id).SingleOrDefault();
            if (animeList != null)
            {
                //TODO: Need to handle image file delete
                //if (animeList.ImageUrl != null)
                //{
                //    if (VirtualPathUtility.IsAppRelative(Server.MapPath(animeList.ImageUrl)))
                //    {
                //        string imagePath = Server.MapPath(animeList.ImageUrl);
                //        if (System.IO.File.Exists(imagePath))
                //        {
                //            System.IO.File.Delete(imagePath);
                //        }
                //    }
                //}
                db.AnimeLists.Remove(animeList);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else return View("ErrorModelNull");
        }

        public ActionResult Explore()
        {
            return View(db.AnimeLists.ToList());
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
