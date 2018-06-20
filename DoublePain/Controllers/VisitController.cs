using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoublePain.Models;

namespace DoublePain.Controllers
{
    public class VisitController : Controller
    {
        private DataManager dataManager = new DataManager();

        public ActionResult VisitCollection()
        {
            if (TempData["User"] == null)
                return RedirectToAction("Index", "Home");

            ViewData["Visits"] = dataManager.Visits.Visits();

            return View();
        }

        public ActionResult Visit(int id)
        {
            if (!(TempData["User"] is Client))
                return RedirectToAction("Index", "Home");

            ViewData.Model = dataManager.Visits.GetVisit(id);

            return View();
        }

        public ActionResult VisitInfo(int id)
        {
            if (!(TempData["User"] is Employee))
                return RedirectToAction("Index", "Home");

            ViewData.Model = dataManager.Visits.GetVisit(id);

            return View();
        }

        public ActionResult Delete(int id)
        {
            if (!(TempData["User"] is Client))
                return RedirectToAction("Index", "Home");

            dataManager.Visits.Delete(id);

            return RedirectToAction("Load", "ClientAccount");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Add()
        {
            if (!(TempData["User"] is Client))
                return RedirectToAction("Index", "Home");

            ViewData["Rooms"] = new SelectList(dataManager.Rooms.Rooms(), "Id", "Number");

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Add(int room, string firstDate, string secondDate)
        {
            DateTime _date1, _date2;
             if (!DateTime.TryParse(firstDate, out _date1) || !DateTime.TryParse(secondDate, out _date2) || _date1 >= _date2)
                ModelState.AddModelError("Date", "Выберите корректные даты");

            if (ModelState.IsValid)
            {
                dataManager.Visits.Add(((Client)TempData["User"]).Id, room, firstDate, secondDate);
                return RedirectToAction("Load", "ClientAccount");
            }

            ViewData["Rooms"] = new SelectList(dataManager.Rooms.Rooms(), "Id", "Number");

            return View();
        }
    }
}