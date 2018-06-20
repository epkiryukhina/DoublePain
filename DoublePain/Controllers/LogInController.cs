using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoublePain.Models;

namespace DoublePain.Controllers
{
    public class LogInController : Controller
    {
        private DataManager dataManager = new DataManager();
        private ModelllContainer db = new ModelllContainer();

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult LogIn()
        {

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult LogIn(string login, string password)
        {
            List<Client> curClient;
            List<Employee> curEmployee;

            curClient = (from a in db.ClientSet where (a.Passport == login) select a).ToList();
            if (curClient.Count != 0)
            {
                if (curClient[0].Password != password)
                    ModelState.AddModelError("Password", "Неверный пароль");
                else
                {
                    TempData["User"] = curClient[0];
                    return RedirectToAction("Load", "ClientAccount");
                }
            }
            else
            {
                curEmployee = (from a in db.EmployeeSet where (a.Passport == login) select a).ToList();
                if (curEmployee.Count == 0)
                    ModelState.AddModelError("Login", "Такого пользователя не существует");
                else
                {
                    if (curEmployee[0].Password != password)
                        ModelState.AddModelError("Password", "Неверный пароль");
                    else
                    {
                        TempData["User"] = curEmployee[0];
                        return RedirectToAction("Load", "EmployeeAccount");
                    }
                }
            }

            return View();
        }

        public ActionResult Exit()
        {
            TempData["User"] = null;

            return RedirectToAction("Index", "Home");
        }
    }
}