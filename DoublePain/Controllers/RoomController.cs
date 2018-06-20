using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoublePain.Models;

namespace DoublePain.Controllers
{
    public class RoomController : Controller
    {
        private DataManager dataManager = new DataManager();

        public ActionResult RoomCollection()
        {
            if (!(TempData["User"] is Employee))
                return RedirectToAction("Index", "Home");

            ViewData["Rooms"] = dataManager.Rooms.Rooms();

            return View();
        }

        public ActionResult Room(int id)
        {
            if (!(TempData["User"] is Employee))
                return RedirectToAction("Index", "Home");

            ViewData.Model = dataManager.Rooms.GetRoom(id);

            return View();
        }

        public ActionResult Delete(int id)
        {
            if (!(TempData["User"] is Employee))
                return RedirectToAction("Index", "Home");

            dataManager.Rooms.Delete(id);

            return RedirectToAction("RoomCollection");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Add()
        {
            if (!(TempData["User"] is Employee))
                return RedirectToAction("Index", "Home");

            ViewData["TypeOfRooms"] = new SelectList(dataManager.TypeOfRooms.TypeOfRooms(), "Id", "Type");

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Add(string number, int typeOfRoom)
        {
            if ((string.IsNullOrWhiteSpace(number)) || (number.Any(b => !Char.IsDigit(b))))
                ModelState.AddModelError("Number", "Введите номер");

            ModelllContainer db = new ModelllContainer();
            if (db.RoomSet.Any(b => b.Number == number))
                ModelState.AddModelError("Number", "Такой номер уже существует");

            if (ModelState.IsValid)
            {
                dataManager.Rooms.Add(number, typeOfRoom);
                return RedirectToAction("RoomCollection");
            }

            ViewData["TypeOfRooms"] = new SelectList(dataManager.TypeOfRooms.TypeOfRooms(), "Id", "Type");

            return View();
        }
    }
}