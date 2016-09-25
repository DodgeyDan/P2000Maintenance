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
    public class TerminalsController : Controller
    {
        private Context db = new Context();

        // GET: Terminals
        public ActionResult Index()
        {
            return View(db.Terminals.ToList());
        }

        // GET: Terminals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Terminal terminal = db.Terminals.Find(id);
            if (terminal == null)
            {
                return HttpNotFound();
            }
            return View(terminal);
        }
        

        // GET: Terminals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Terminal terminal = db.Terminals.Find(id);
            if (terminal == null)
            {
                return HttpNotFound();
            }
            return View(terminal);
        }

        // POST: Terminals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Terminal terminal = db.Terminals.Find(id);
            db.Terminals.Remove(terminal);
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
            P2000ContextModules.PopulateTerminalsFromP2000();
            return RedirectToAction("Index");
        }
    }
}
