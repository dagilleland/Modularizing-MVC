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
    public class ShipDesignsController : Controller
    {
        private ShipyardRepository db = new ShipyardRepository();

        // GET: Fleet/ShipDesigns
        public ActionResult Index()
        {
            return View(db.Designs.ToList());
        }

        // GET: Fleet/ShipDesigns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShipDesign shipDesign = db.Designs.Find(id);
            if (shipDesign == null)
            {
                return HttpNotFound();
            }
            return View(shipDesign);
        }

        // GET: Fleet/ShipDesigns/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fleet/ShipDesigns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShipDesignId,Name,CommissionedDate,StandardCrewComplement")] ShipDesign shipDesign)
        {
            if (ModelState.IsValid)
            {
                db.Designs.Add(shipDesign);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(shipDesign);
        }

        // GET: Fleet/ShipDesigns/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShipDesign shipDesign = db.Designs.Find(id);
            if (shipDesign == null)
            {
                return HttpNotFound();
            }
            return View(shipDesign);
        }

        // POST: Fleet/ShipDesigns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShipDesignId,Name,CommissionedDate,StandardCrewComplement")] ShipDesign shipDesign)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shipDesign).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shipDesign);
        }

        // GET: Fleet/ShipDesigns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShipDesign shipDesign = db.Designs.Find(id);
            if (shipDesign == null)
            {
                return HttpNotFound();
            }
            return View(shipDesign);
        }

        // POST: Fleet/ShipDesigns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ShipDesign shipDesign = db.Designs.Find(id);
            db.Designs.Remove(shipDesign);
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
