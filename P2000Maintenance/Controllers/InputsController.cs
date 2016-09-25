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
    public class InputsController : Controller
    {
        private Context db = new Context();

        // GET: Inputs
        public ActionResult Index()
        {
            return View(db.Inputs.ToList());
        }

        // GET: Inputs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Input input = db.Inputs.Find(id);
            if (input == null)
            {
                return HttpNotFound();
            }
            return View(input);
        }

        // GET: Inputs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inputs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ip_partition,Name,Number,Enabled,SoftInput,LastMaintained,Technician")] Input input)
        {
            if (ModelState.IsValid)
            {
                db.Inputs.Add(input);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(input);
        }

        // GET: Inputs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Input input = db.Inputs.Find(id);
            if (input == null)
            {
                return HttpNotFound();
            }
            return View(input);
        }

        // POST: Inputs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ip_partition,Name,Number,Enabled,SoftInput,LastMaintained,Technician")] Input input)
        {
            if (ModelState.IsValid)
            {
                db.Entry(input).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(input);
        }

        // GET: Inputs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Input input = db.Inputs.Find(id);
            if (input == null)
            {
                return HttpNotFound();
            }
            return View(input);
        }

        // POST: Inputs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Input input = db.Inputs.Find(id);
            db.Inputs.Remove(input);
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
            P2000ContextModules.PopulateInputsFromP2000();
            return RedirectToAction("Index");
        }
    }
}
