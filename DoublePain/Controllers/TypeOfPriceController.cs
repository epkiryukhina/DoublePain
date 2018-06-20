using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoublePain.Models;

namespace DoublePain.Controllers
{
    public class TypeOfPriceController : Controller
    {
        private DataManager dataManager = new DataManager();

        public ActionResult TypeOfPriceCollection()
        {
            if (!(TempData["User"] is Employee))
                return RedirectToAction("Index", "Home");

            ViewData["TypeOfPrices"] = dataManager.TypeOfPrices.TypeOfPrices();

            return View();
        }

        public ActionResult TypeOfPrice(int id)
        {
            if (!(TempData["User"] is Employee))
                return RedirectToAction("Index", "Home");

            ViewData.Model = dataManager.TypeOfPrices.GetTypeOfPrice(id);

            return View();
        }

        public ActionResult Delete(int id)
        {
            if (!(TempData["User"] is Employee))
                return RedirectToAction("Index", "Home");

            dataManager.TypeOfPrices.Delete(id);

            return RedirectToAction("TypeOfPriceCollection");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Add()
        {
            if (!(TempData["User"] is Employee))
                return RedirectToAction("Index", "Home");

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Add(string type)
        {
            if (ModelState.IsValid)
            {
                dataManager.TypeOfPrices.Add(type);
                return RedirectToAction("TypeOfPriceCollection");
            }

            return View();
        }
    }
}