using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Shipyards.UI.Areas.Fleet.Models;

namespace Shipyards.UI.Areas.Fleet.Controllers
{
    public class ShipsController : Controller
    {
        private ShipyardRepository db = new ShipyardRepository();

        // GET: Fleet/Ships
        public ActionResult Index()
        {
            var ships = db.Ships.Include(s => s.Class);
            return View(ships.ToList());
        }

        // GET: Fleet/Ships/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ship ship = db.Ships.Find(id);
            if (ship == null)
            {
                return HttpNotFound();
            }
            return View(ship);
        }

        // GET: Fleet/Ships/Create
        public ActionResult Create()
        {
            ViewBag.ShipDesignId = new SelectList(db.Designs, "ShipDesignId", "Name");
            return View();
        }

        // POST: Fleet/Ships/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RegistryNumber,Name,LaunchDate,ShipDesignId")] Ship ship)
        {
            if (ModelState.IsValid)
            {
                db.Ships.Add(ship);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ShipDesignId = new SelectList(db.Designs, "ShipDesignId", "Name", ship.ShipDesignId);
            return View(ship);
        }

        // GET: Fleet/Ships/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ship ship = db.Ships.Find(id);
            if (ship == null)
            {
                return HttpNotFound();
            }
            ViewBag.ShipDesignId = new SelectList(db.Designs, "ShipDesignId", "Name", ship.ShipDesignId);
            return View(ship);
        }

        // POST: Fleet/Ships/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RegistryNumber,Name,LaunchDate,ShipDesignId")] Ship ship)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ship).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ShipDesignId = new SelectList(db.Designs, "ShipDesignId", "Name", ship.ShipDesignId);
            return View(ship);
        }

        // GET: Fleet/Ships/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ship ship = db.Ships.Find(id);
            if (ship == null)
            {
                return HttpNotFound();
            }
            return View(ship);
        }

        // POST: Fleet/Ships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Ship ship = db.Ships.Find(id);
            db.Ships.Remove(ship);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
