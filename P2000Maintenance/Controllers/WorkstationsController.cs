using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using P2000Maintenance.Models.P2000;
using P2000Maintenance.Models;

namespace P2000Maintenance.Controllers
{
    public class WorkstationsController : Controller
    {
        private Context db = new Context();

        // GET: Workstations
        public ActionResult Index()
        {
            return View(db.Workstations.ToList());
        }

        // GET: Workstations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workstation workstation = db.Workstations.Find(id);
            if (workstation == null)
            {
                return HttpNotFound();
            }
            return View(workstation);
        }

        // GET: Workstations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Workstations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,dbId,Enabled,Badging,Server,Version,OnlineDate,Ip_Address,Guid")] Workstation workstation)
        {
            if (ModelState.IsValid)
            {
                db.Workstations.Add(workstation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(workstation);
        }

        // GET: Workstations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workstation workstation = db.Workstations.Find(id);
            if (workstation == null)
            {
                return HttpNotFound();
            }
            return View(workstation);
        }

        // POST: Workstations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,dbId,Enabled,Badging,Server,Version,OnlineDate,Ip_Address,Guid")] Workstation workstation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workstation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(workstation);
        }

        // GET: Workstations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workstation workstation = db.Workstations.Find(id);
            if (workstation == null)
            {
                return HttpNotFound();
            }
            return View(workstation);
        }

        // POST: Workstations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Workstation workstation = db.Workstations.Find(id);
            db.Workstations.Remove(workstation);
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

        public ActionResult Populate()
        {
            P2000ContextModules.PopulateWorkstationsFromP2000();
            return RedirectToAction("Index");
        }

        public ActionResult TestStations()
        {
            P2000ContextModules.TestWorkstations();
            return RedirectToAction("Index");
        }


    }
}
