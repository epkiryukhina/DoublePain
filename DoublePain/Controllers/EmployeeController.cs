using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoublePain.Models;

namespace DoublePain.Controllers
{
    public class EmployeeController : Controller
    {
        private DataManager dataManager = new DataManager();

        public ActionResult EmployeeCollection()
        {
            if (!(TempData["User"] is Employee))
                return RedirectToAction("Index", "Home");

            ViewData["Employees"] = dataManager.Employees.Employees();

            return View();
        }

        public ActionResult Employee(int id)
        {
            if (!(TempData["User"] is Employee))
                return RedirectToAction("Index", "Home");

            ViewData.Model = dataManager.Employees.GetEmployee(id);

            return View();
        }

        public ActionResult EmployeeInfo(int id)
        {
            if (!(TempData["User"] is Employee))
                return RedirectToAction("Index", "Home");

            ViewData.Model = dataManager.Employees.GetEmployee(id);

            return View();
        }

        public ActionResult Delete()
        {
            int id = ((Employee)TempData["User"]).Id;

            TempData["User"] = null;

            dataManager.Employees.Delete(id);

            return RedirectToAction("Index", "Home");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Add()
        {
            ViewData["Jobs"] = new SelectList(dataManager.Jobs.Jobs(), "Id", "JobName");
            ViewData["TypeOfServices"] = new SelectList(dataManager.TypeOfServices.TypeOfServices(), "Id", "Name");

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Add(string name, string date, string sex, string passport, string password, int job, int typeOfService)
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
            if (db.EmployeeSet.Any(b => b.Passport == passport))
                ModelState.AddModelError("Passport", "Такой пользователь уже зарегистрирован");

            if (ModelState.IsValid)
            {
                dataManager.Employees.Add(name, date, sex, passport, password, job, typeOfService);
                return RedirectToAction("EmployeeCollection");
            }

            ViewData["Jobs"] = new SelectList(dataManager.Jobs.Jobs(), "Id", "JobName");
            ViewData["TypeOfServices"] = new SelectList(dataManager.TypeOfServices.TypeOfServices(), "Id", "Name");

            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit()
        {
            int id = ((Employee)TempData["User"]).Id;

            Employee c = dataManager.Employees.GetEmployee(id);
            TempData["User"] = c;
            ViewData.Model = c;

            List<SelectListItem> jobs = new List<SelectListItem>();
            List<SelectListItem> typeOfServices = new List<SelectListItem>();

            foreach (Job a in dataManager.Jobs.Jobs())
            {
                jobs.Add(new SelectListItem { Text = a.JobName, Value = a.Id.ToString(), Selected = c.Job.Id == a.Id });
            }

            foreach (TypeOfService a in dataManager.TypeOfServices.TypeOfServices())
            {
                typeOfServices.Add(new SelectListItem { Text = a.Name, Value = a.Id.ToString(), Selected = c.TypeOfService.Id == a.Id });
            }

            ViewBag.Job = jobs;
            ViewBag.TypeOfService = typeOfServices;

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int id, string name, string date, string sex, string passport, string password, int job, int typeOfService)
        {
            List<SelectListItem> jobs = new List<SelectListItem>();
            List<SelectListItem> typeOfServices = new List<SelectListItem>();
            Employee c = dataManager.Employees.GetEmployee(id);

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
            if (db.EmployeeSet.Any(b => b.Passport == passport && b.Passport != c.Passport))
                ModelState.AddModelError("Passport", "Такой пользователь уже зарегистрирован");

            if (ModelState.IsValid)
            {
                dataManager.Employees.Edit(id, name, date, sex, passport, password, job, typeOfService);
                return RedirectToAction("Load", "EmployeeAccount");
            }

            c = dataManager.Employees.GetEmployee(id);
            TempData["User"] = c;

            foreach (Job a in dataManager.Jobs.Jobs())
            {
                jobs.Add(new SelectListItem { Text = a.JobName, Value = a.Id.ToString(), Selected = c.Job.Id == a.Id });
            }

            foreach (TypeOfService a in dataManager.TypeOfServices.TypeOfServices())
            {
                typeOfServices.Add(new SelectListItem { Text = a.Name, Value = a.Id.ToString(), Selected = c.TypeOfService.Id == a.Id });
            }

            ViewData.Model = c;
            ViewBag.Job = jobs;
            ViewBag.TypeOfService = typeOfServices;

            return View();
        }
    }
}