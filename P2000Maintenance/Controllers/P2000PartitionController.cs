using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using P2000Maintenance.Models;
using System.Data.SqlClient;
using P2000Maintenance.Models.P2000;

namespace P2000Maintenance.Controllers
{
    public class P2000PartitionController : Controller
    {
        private Context db = new Context();

        // GET: P2000Partition
        public ActionResult Index()
        {
            return View(db.Partitions.ToList());
        }

        // GET: P2000Partition/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partition p2000Partition = db.Partitions.Find(id);
            if (p2000Partition == null)
            {
                return HttpNotFound();
            }
            return View(p2000Partition);
        }

        // GET: P2000Partition/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: P2000Partition/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Partition p2000Partition)
        {
            if (ModelState.IsValid)
            {
                db.Partitions.Add(p2000Partition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(p2000Partition);
        }

        // GET: P2000Partition/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partition p2000Partition = db.Partitions.Find(id);
            if (p2000Partition == null)
            {
                return HttpNotFound();
            }
            return View(p2000Partition);
        }

        // POST: P2000Partition/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Partition p2000Partition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(p2000Partition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(p2000Partition);
        }

        // GET: P2000Partition/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partition p2000Partition = db.Partitions.Find(id);
            if (p2000Partition == null)
            {
                return HttpNotFound();
            }
            return View(p2000Partition);
        }

        // POST: P2000Partition/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Partition p2000Partition = db.Partitions.Find(id);
            db.Partitions.Remove(p2000Partition);
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
        

        // GET: P2000Partition
        public ActionResult Populate()
        {
            P2000ContextModules.PopulatePartitionsFromP2000();
            return RedirectToAction("Index");
        }
    }
}
