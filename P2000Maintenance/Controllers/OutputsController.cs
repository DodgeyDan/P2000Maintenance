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
    public class OutputsController : Controller
    {
        private Context db = new Context();

        // GET: Outputs
        public ActionResult Index()
        {
            return View(db.Outputs.ToList());
        }

        // GET: Outputs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Output output = db.Outputs.Find(id);
            if (output == null)
            {
                return HttpNotFound();
            }
            return View(output);
        }

        // GET: Outputs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Outputs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,dbId,Name,Number,Enabled,Guid")] Output output)
        {
            if (ModelState.IsValid)
            {
                db.Outputs.Add(output);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(output);
        }

        // GET: Outputs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Output output = db.Outputs.Find(id);
            if (output == null)
            {
                return HttpNotFound();
            }
            return View(output);
        }

        // POST: Outputs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,dbId,Name,Number,Enabled,Guid")] Output output)
        {
            if (ModelState.IsValid)
            {
                db.Entry(output).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(output);
        }

        // GET: Outputs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Output output = db.Outputs.Find(id);
            if (output == null)
            {
                return HttpNotFound();
            }
            return View(output);
        }

        // POST: Outputs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Output output = db.Outputs.Find(id);
            db.Outputs.Remove(output);
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
            P2000ContextModules.PopulateOutputsFromP2000();
            return RedirectToAction("Index");
        }
    }
}
