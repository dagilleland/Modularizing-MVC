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
    public class CrewMembersController : Controller
    {
        private ShipyardRepository db = new ShipyardRepository();

        // GET: Fleet/CrewMembers
        public ActionResult Index()
        {
            var crewMembers = db.CrewMembers.Include(c => c.Assignment);
            return View(crewMembers.ToList());
        }

        // GET: Fleet/CrewMembers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CrewMember crewMember = db.CrewMembers.Find(id);
            if (crewMember == null)
            {
                return HttpNotFound();
            }
            return View(crewMember);
        }

        // GET: Fleet/CrewMembers/Create
        public ActionResult Create()
        {
            ViewBag.ShipRegistry = new SelectList(db.Ships, "RegistryNumber", "Name");
            return View();
        }

        // POST: Fleet/CrewMembers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FullName,Rank,Division,ShipRegistry")] CrewMember crewMember)
        {
            if (ModelState.IsValid)
            {
                db.CrewMembers.Add(crewMember);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ShipRegistry = new SelectList(db.Ships, "RegistryNumber", "Name", crewMember.ShipRegistry);
            return View(crewMember);
        }

        // GET: Fleet/CrewMembers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CrewMember crewMember = db.CrewMembers.Find(id);
            if (crewMember == null)
            {
                return HttpNotFound();
            }
            ViewBag.ShipRegistry = new SelectList(db.Ships, "RegistryNumber", "Name", crewMember.ShipRegistry);
            return View(crewMember);
        }

        // POST: Fleet/CrewMembers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FullName,Rank,Division,ShipRegistry")] CrewMember crewMember)
        {
            if (ModelState.IsValid)
            {
                db.Entry(crewMember).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ShipRegistry = new SelectList(db.Ships, "RegistryNumber", "Name", crewMember.ShipRegistry);
            return View(crewMember);
        }

        // GET: Fleet/CrewMembers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CrewMember crewMember = db.CrewMembers.Find(id);
            if (crewMember == null)
            {
                return HttpNotFound();
            }
            return View(crewMember);
        }

        // POST: Fleet/CrewMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CrewMember crewMember = db.CrewMembers.Find(id);
            db.CrewMembers.Remove(crewMember);
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
