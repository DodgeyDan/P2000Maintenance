using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using P2000Maintenance.Models;
using P2000Maintenance.Models.P2000;

namespace P2000Maintenance.Controllers
{
    public class P2000PanelController : P2000BaseController
    {
        private Context db = new Context();

        // GET: P2000Panel
        public async Task<ActionResult> Index()
        {
            return View(await db.Panels.ToListAsync());
        }

        // GET: P2000Panel/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Panel p2000Panel = await db.Panels.FindAsync(id);
            if (p2000Panel == null)
            {
                return HttpNotFound();
            }
            return View(p2000Panel);
        }

        // GET: P2000Panel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: P2000Panel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Model,Version,Ip_Address,Guid")] Panel p2000Panel)
        {
            if (ModelState.IsValid)
            {
                db.Panels.Add(p2000Panel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(p2000Panel);
        }

        // GET: P2000Panel/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Panel p2000Panel = await db.Panels.FindAsync(id);
            if (p2000Panel == null)
            {
                return HttpNotFound();
            }
            return View(p2000Panel);
        }

        // POST: P2000Panel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Model,Version,Ip_Address,Guid")] Panel p2000Panel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(p2000Panel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(p2000Panel);
        }

        // GET: P2000Panel/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Panel p2000Panel = await db.Panels.FindAsync(id);
            if (p2000Panel == null)
            {
                return HttpNotFound();
            }
            return View(p2000Panel);
        }

        // POST: P2000Panel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Panel p2000Panel = await db.Panels.FindAsync(id);
            db.Panels.Remove(p2000Panel);
            await db.SaveChangesAsync();
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
