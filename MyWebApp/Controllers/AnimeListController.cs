using MyWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            try
            {
                db.AnimeLists.Add(animeList);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
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
    }
}
