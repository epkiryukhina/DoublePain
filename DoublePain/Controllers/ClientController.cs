using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoublePain.Models;

namespace DoublePain.Controllers
{
    public class ClientController : Controller
    {
        private DataManager dataManager = new DataManager();

        public ActionResult ClientCollection()
        {
            if (!(TempData["User"] is Employee))
                return RedirectToAction("Index", "Home");

            ViewData["Clients"] = dataManager.Clients.Clients();

            return View();
        }

        public ActionResult Client(int id)
        {
            if (!(TempData["User"] is Client))
                return RedirectToAction("Index", "Home");

            ViewData.Model = dataManager.Clients.GetClient(id);

            return View();
        }


        public ActionResult ClientInfo(int id)
        {
            if (!(TempData["User"] is Employee))
                return RedirectToAction("Index", "Home");

            ViewData.Model = dataManager.Clients.GetClient(id);

            return View();
        }

        public ActionResult Delete()
        {
            int id = ((Client)TempData["User"]).Id;
            TempData["User"] = null;

            dataManager.Clients.Delete(id);

            return RedirectToAction("Index", "Home");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Add()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Add(string name, string date, string sex, string passport, string password)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Any(b => char.IsDigit(b)) || name.Split(' ').Length != 3)
                ModelState.AddModelError("Name", "Введите полное корректное ФИО");

            DateTime _date;
            if (!DateTime.TryParse(date, out _date))
                ModelState.AddModelError("Date", "Выберите корректную дату");

            if (sex != "Мужчина" && sex != "Женщина")
                ModelState.AddModelError("Sex", "Пол введен не верно");

            if (passport.Length != 10 || passport.Any(b => !Char.IsDigit(b)))
                ModelState.AddModelError("Passport", "Некорректные паспортные данные");

            ModelllContainer db = new ModelllContainer();
            if (db.ClientSet.Any(b => b.Passport == passport))
                ModelState.AddModelError("Passport", "Такой пользователь уже зарегистрирован");

            if (ModelState.IsValid)
            {
                dataManager.Clients.Add(name, date, sex, passport, password);
                Client c = (from b in db.ClientSet where (b.Passport == passport) select b).First();
                TempData["User"] = c;
                return RedirectToAction("Load", "ClientAccount");
            }

            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit()
        {
            if (!(TempData["User"] is Client))
                return RedirectToAction("Index", "Home");

            int id = ((Client)TempData["User"]).Id;

            Client c = dataManager.Clients.GetClient(id);
            TempData["User"] = c;

            ViewData.Model = c;

            List<SelectListItem> visits = new List<SelectListItem>();

            foreach (Visit a in dataManager.Visits.Visits())
            {
                visits.Add(new SelectListItem { Text = a.FirstDate.ToString(), Value = a.Id.ToString(), Selected = c.Visit.Any(b => b.Id == a.Id) });
            }

            ViewBag.Visit = visits;

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int id, string name, string date, string sex, string passport, string password)
        {
            List<SelectListItem> visits = new List<SelectListItem>();
            Client c = dataManager.Clients.GetClient(id);

            if (string.IsNullOrWhiteSpace(name) || name.Any(b => char.IsDigit(b)) || name.Split(' ').Length != 3)
                ModelState.AddModelError("Name", "Введите полное корректное ФИО");

            DateTime _date;
            if (!DateTime.TryParse(date, out _date))
                ModelState.AddModelError("Date", "Выберите корректную дату");

            if (sex != "Мужчина" && sex != "Женщина")
                ModelState.AddModelError("Sex", "Пол введен не верно");

            if (passport.Length != 10 || passport.Any(b => !Char.IsDigit(b)))
                ModelState.AddModelError("Passport", "Некорректные паспортные данные");

            ModelllContainer db = new ModelllContainer();
            if (db.ClientSet.Any(b => b.Passport == passport && b.Passport != c.Passport))
                ModelState.AddModelError("Passport", "Такой пользователь уже зарегистрирован");

            if (ModelState.IsValid)
            {
                dataManager.Clients.Edit(id, name, date, sex, passport, password);
                return RedirectToAction("Load", "ClientAccount");
            }

            c = dataManager.Clients.GetClient(id);
            TempData["User"] = c;

            foreach (Visit a in dataManager.Visits.Visits())
            {
                visits.Add(new SelectListItem { Text = a.FirstDate.ToString(), Value = a.Id.ToString(), Selected = c.Visit.Any(b => b.Id == a.Id) });
            }

            ViewData.Model = c;
            ViewBag.Visit = visits;

            return View();
        }
    }
}