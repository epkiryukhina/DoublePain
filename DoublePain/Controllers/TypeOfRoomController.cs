using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoublePain.Models;

namespace DoublePain.Controllers
{
    public class TypeOfRoomController : Controller
    {
        private DataManager dataManager = new DataManager();

        public ActionResult TypeOfRoomCollection()
        {
            if (!(TempData["User"] is Employee))
                return RedirectToAction("Index", "Home");

            ViewData["TypeOfRooms"] = dataManager.TypeOfRooms.TypeOfRooms();

            return View();
        }

        public ActionResult TypeOfRoom(int id)
        {
            if (!(TempData["User"] is Employee))
                return RedirectToAction("Index", "Home");

            ViewData.Model = dataManager.TypeOfRooms.GetTypeOfRoom(id);

            return View();
        }

        public ActionResult Delete(int id)
        {
            if (!(TempData["User"] is Employee))
                return RedirectToAction("Index", "Home");

            dataManager.TypeOfRooms.Delete(id);

            return RedirectToAction("TypeOfRoomCollection");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Add()
        {
            if (!(TempData["User"] is Employee))
                return RedirectToAction("Index", "Home");

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Add(string type, string price, string capacity)
        {
            int curPrice, curCapacity;

            if (!Int32.TryParse(price, out curPrice))
                ModelState.AddModelError("Price", "Цена должна быть числом");

            if (!Int32.TryParse(capacity, out curCapacity))
                ModelState.AddModelError("Capacity", "Вместимость должна быть числом");

            if (ModelState.IsValid)
            {
                dataManager.TypeOfRooms.Add(type, curPrice, curCapacity);
                return RedirectToAction("TypeOfRoomCollection");
            }

            return View();
        }
    }
}