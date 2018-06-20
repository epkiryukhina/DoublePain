using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoublePain.Models;

namespace DoublePain.Controllers
{
    public class EmployeeAccountController : Controller
    {
        private DataManager dataManager = new DataManager();
        private ModelllContainer db = new ModelllContainer();

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Load()
        {
            ViewData["Clients"] = (from a in db.ClientSet select a).ToArray();
            ViewData["Employees"] = (from a in db.EmployeeSet select a).ToArray();

            if (TempData["User"] == null)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Load(string firstDate, string secondDate, int typeOfRoom, int numberOfPeople, int minPrice, int maxPrice)
        {

            return View();
        }
    }
}