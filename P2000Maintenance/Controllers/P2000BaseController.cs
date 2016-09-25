using P2000Maintenance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace P2000Maintenance.Controllers
{
    public class P2000BaseController : Controller
    {

        /*
         * Populate all P2000 Tables and redirect to Index
         * 
         * Add the below to Index to inherit.
         *  @Html.ActionLink("Populate", "Populate")
         */
        public ActionResult Populate()
        {
            P2000ContextModules.PopulateAllFromP2000();
            return RedirectToAction("Index");
        }
    }
}