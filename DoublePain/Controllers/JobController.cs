using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoublePain.Models;

namespace DoublePain.Controllers
{
    public class JobController : Controller
    {
        private DataManager dataManager = new DataManager();

        public ActionResult JobCollection()
        {
            if (!(TempData["User"] is Employee))
                return RedirectToAction("Index", "Home");

            ViewData["Jobs"] = dataManager.Jobs.Jobs();

            return View();
        }

        public ActionResult Job(int id)
        {
            if (!(TempData["User"] is Employee))
                return RedirectToAction("Index", "Home");

            ViewData.Model = dataManager.Jobs.GetJob(id);

            return View();
        }

        public ActionResult Delete(int id)
        {
            if (!(TempData["User"] is Employee))
                return RedirectToAction("Index", "Home");

            dataManager.Jobs.Delete(id);

            return RedirectToAction("JobCollection");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Add()
        {
            if (!(TempData["User"] is Employee))
                return RedirectToAction("Index", "Home");

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Add(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                ModelState.AddModelError("Name", "Введите название должности");

            if (ModelState.IsValid)
            {
                dataManager.Jobs.Add(name);
                return RedirectToAction("JobCollection");
            }

            return View();
        }
    }
}