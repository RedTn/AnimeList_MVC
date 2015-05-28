using MyWebApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace MyWebApp.Controllers
{
    [Authorize]
    public class AnimeListController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AnimeList
        public ActionResult Index()
        {
            return View();
        }

        // GET: AnimeList/Details/5
        public ActionResult Details(int id)
        {
            return View();
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

                //TODO: Find better way to do path literals
                var uploadDir = @"/Content/Images/AnimeList/";

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
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Explore()
        {
            return View(db.AnimeLists.ToList());
        }
    }
}
