using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoublePain.Models;

namespace DoublePain.Controllers
{
    public class ServiceController : Controller
    {
        private DataManager dataManager = new DataManager();

        public ActionResult ServiceCollection()
        {
            if (TempData["User"] == null)
                return RedirectToAction("Index", "Home");

            ViewData["Services"] = dataManager.Services.Services();

            return View();
        }

        public ActionResult Service(int id)
        {
            if (!(TempData["User"] is Client))
                return RedirectToAction("Index", "Home");

            ViewData.Model = dataManager.Services.GetService(id);

            return View();
        }

        public ActionResult ServiceInfo(int id)
        {
            if (!(TempData["User"] is Employee))
                return RedirectToAction("Index", "Home");

            ViewData.Model = dataManager.Services.GetService(id);

            return View();
        }

        public ActionResult Delete(int id)
        {
            if (!(TempData["User"] is Client))
                return RedirectToAction("Index", "Home");

            dataManager.Services.Delete(id);

            return RedirectToAction("ServiceCollection");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Add()
        {
            if (!(TempData["User"] is Client))
                return RedirectToAction("Index", "Home");

            ViewData["Clients"] = new SelectList(dataManager.Clients.Clients(), "Id", "Name");
            ViewData["Employees"] = new SelectList(dataManager.Employees.Employees(), "Id", "Name");

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Add(int employee, string date, string numberOfHours, string numberOfPeople)
        {
            DateTime _date;
            if (!DateTime.TryParse(date, out _date))
                ModelState.AddModelError("Date", "Выберите корректную дату");

            if ((string.IsNullOrWhiteSpace(numberOfHours)) || (numberOfHours.Any(b => !Char.IsDigit(b)) || Int32.Parse(numberOfHours) < 0 || Int32.Parse(numberOfHours) > 7))
                ModelState.AddModelError("NumberOfHours", "Введите число от 0 до 7");

            if ((string.IsNullOrWhiteSpace(numberOfPeople)) || (numberOfPeople.Any(b => !Char.IsDigit(b)) || Int32.Parse(numberOfPeople) < 0 || Int32.Parse(numberOfPeople) > 10))
                ModelState.AddModelError("NumberOfPeople", "Введите число от 0 до 10");

            if (ModelState.IsValid)
            {
                dataManager.Services.Add(((Client)TempData["User"]).Id, employee, date, Int32.Parse(numberOfHours), Int32.Parse(numberOfPeople));
                return RedirectToAction("Load", "ClientAccount");
            }

            ViewData["Employees"] = new SelectList(dataManager.Employees.Employees(), "Id", "Name");

            return View();
        }
    }
}