using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoublePain.Models;

namespace DoublePain.Controllers
{
    public class TypeOfServiceController : Controller
    {
        private DataManager dataManager = new DataManager();

        public ActionResult TypeOfServiceCollection()
        {
            if (!(TempData["User"] is Employee))
                return RedirectToAction("Index", "Home");

            ViewData["TypeOfServices"] = dataManager.TypeOfServices.TypeOfServices();

            return View();
        }

        public ActionResult TypeOfService(int id)
        {
            if (!(TempData["User"] is Employee))
                return RedirectToAction("Index", "Home");

            ViewData.Model = dataManager.TypeOfServices.GetTypeOfService(id);

            return View();
        }

        public ActionResult Delete(int id)
        {
            if (!(TempData["User"] is Employee))
                return RedirectToAction("Index", "Home");

            dataManager.TypeOfServices.Delete(id);

            return RedirectToAction("TypeOfServiceCollection");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Add()
        {
            if (!(TempData["User"] is Employee))
                return RedirectToAction("Index", "Home");

            ViewData["TypeOfPrices"] = new SelectList(dataManager.TypeOfPrices.TypeOfPrices(), "Id", "Type");

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Add(string name, int typeOfPrice, string price)
        {
            int curPrice;

            if (!Int32.TryParse(price, out curPrice))
                ModelState.AddModelError("Price", "Цена должна быть числом");

            if (ModelState.IsValid)
            {
                dataManager.TypeOfServices.Add(name, typeOfPrice, curPrice);
                return RedirectToAction("TypeOfServiceCollection");
            }

            ViewData["TypeOfPrices"] = new SelectList(dataManager.TypeOfPrices.TypeOfPrices(), "Id", "Type");

            return View();
        }
    }
}