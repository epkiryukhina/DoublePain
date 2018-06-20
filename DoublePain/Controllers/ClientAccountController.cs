using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoublePain.Models;

namespace DoublePain.Controllers
{
        public class ClientAccountController : Controller
    {
        private DataManager dataManager = new DataManager();
        private ModelllContainer db = new ModelllContainer();

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Load()
        {
            if (!(TempData["User"] is Client))
                return RedirectToAction("Index", "Home");

            Client c = (Client)TempData["User"];
            TempData["User"] = c;
            ViewData.Model = c;

            ViewData["YourVisits"] = (from a in db.VisitSet where (a.Client.Id == c.Id) select a).ToArray();
            ViewData["YourServices"] = (from a in db.ServiceSet where (a.Client.Id == c.Id) select a).ToArray();
            ViewData["TypeOfRooms"] = new SelectList(dataManager.TypeOfRooms.TypeOfRooms(), "Id", "Type");
            ViewData["TypeOfServices"] = new SelectList(dataManager.TypeOfServices.TypeOfServices(), "Id", "Name");
            ViewData["TypeOfPrices"] = new SelectList(dataManager.TypeOfPrices.TypeOfPrices(), "Id", "Type");

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Load(List<Room> list)
        {

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ShowVisits(Client model, string firstDate, string secondDate, int typeOfRoom, string numberOfPeople, string minPrice, string maxPrice)
        {
            Client c = (Client)TempData["User"];
            TempData["User"] = c;

            DateTime _date1;
            DateTime _date2;
            DateTime.TryParse(firstDate, out _date1);
            DateTime.TryParse(secondDate, out _date2);
            if (_date1 == DateTime.Parse("01.01.0001") || _date2 == DateTime.Parse("01.01.0001") || _date1 >= _date2)
                ModelState.AddModelError("FirstDate", "Выберите корректные даты");

            int curNum, curMaxPrice, curMinPrice;
            if (!(Int32.TryParse(numberOfPeople, out curNum)))
                ModelState.AddModelError("NumberOfPeople", "Количество людей должно быть целым числом");

            if (!(Int32.TryParse(minPrice, out curMinPrice)))
                ModelState.AddModelError("MinPrice1", "Минимальная цена должна быть целым числом");

            if (!(Int32.TryParse(maxPrice, out curMaxPrice)))
                ModelState.AddModelError("MaxPrice1", "Максимальная цена должна быть целым числом");

            if (curMaxPrice < curMinPrice && curMinPrice != 0 && curMaxPrice !=0)
                ModelState.AddModelError("MinPrice1", "Минимальная цена должна быть меньше максимальной");

            if (ModelState.IsValid)
            {
                List<Visit> first = new List<Visit>();
                List<Visit> second = new List<Visit>();
                List<int> res = new List<int>();

                first = (from v in db.VisitSet where (v.Client.Id == c.Id && v.FirstDate <= _date1 && v.SecondDate >= _date2) select v).ToList();
                if (typeOfRoom > 0)
                {
                    foreach (Visit x in first)
                        if (x.Room.TypeOfRoom.Id == typeOfRoom)
                            second.Add(x);

                    first = second;
                    second = new List<Visit>();
                }
                if (curNum > 0)
                {
                    foreach (Visit x in first)
                        if (x.Room.TypeOfRoom.Capacity >= curNum)
                            second.Add(x);

                    first = second;
                    second = new List<Visit>();
                }
                if (curMinPrice > 0)
                {
                    foreach (Visit x in first)
                        if (x.Room.TypeOfRoom.Price >= curMinPrice)
                            second.Add(x);

                    first = second;
                    second = new List<Visit>();
                }
                if (curMaxPrice > 0)
                {
                    foreach (Visit x in first)
                        if (x.Room.TypeOfRoom.Price <= curMaxPrice)
                            second.Add(x);

                    first = second;
                }

                foreach (Visit x in first)
                    res.Add(x.Id);

                ViewData["YourVisits"] = (from a in db.VisitSet where (res.Any(b => b == a.Id)) select a).ToList();
            }
            else ViewData["YourVisits"] = (from a in db.VisitSet where (a.Client.Id == c.Id) select a).ToArray();
            ViewData["YourServices"] = (from a in db.ServiceSet where (a.Client.Id == c.Id) select a).ToArray();
            ViewData["TypeOfRooms"] = new SelectList(dataManager.TypeOfRooms.TypeOfRooms(), "Id", "Type");
            ViewData["TypeOfServices"] = new SelectList(dataManager.TypeOfServices.TypeOfServices(), "Id", "Name");
            ViewData["TypeOfPrices"] = new SelectList(dataManager.TypeOfPrices.TypeOfPrices(), "Id", "Type");

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ShowServices(Client model, string date, int typeOfService, int typeOfPrice, string minPrice, string maxPrice)
        {
            Client c = (Client)TempData["User"];
            TempData["User"] = c;

            DateTime _date;
            DateTime.TryParse(date, out _date);
            if (_date == DateTime.Parse("01.01.0001"))
                ModelState.AddModelError("Date", "Выберите корректную даты");

            int curMaxPrice, curMinPrice;
            if (!(Int32.TryParse(minPrice, out curMinPrice)))
                ModelState.AddModelError("MinPrice", "Минимальная цена должна быть целым числом");

            if (!(Int32.TryParse(maxPrice, out curMaxPrice)))
                ModelState.AddModelError("MaxPrice", "Максимальная цена должна быть целым числом");

            if (curMaxPrice < curMinPrice && curMinPrice != 0 && curMaxPrice != 0)
                ModelState.AddModelError("MinPrice", "Минимальная цена должна быть меньше максимальной");

            if (ModelState.IsValid)
            {
                List<Service> first = new List<Service>();
                List<Service> second = new List<Service>();
                List<int> res = new List<int>();

                first = (from v in db.ServiceSet where (v.Client.Id == c.Id && v.Date == _date) select v).ToList();
                if (typeOfService > 0)
                {
                    foreach (Service x in first)
                        if (x.Employee.TypeOfService.Id == typeOfService)
                            second.Add(x);

                    first = second;
                    second = new List<Service>();
                }
                if (typeOfPrice > 0)
                {
                    foreach (Service x in first)
                        if (x.Employee.TypeOfService.TypeOfPrice.Id == typeOfPrice)
                            second.Add(x);

                    first = second;
                    second = new List<Service>();
                }
                if (curMinPrice > 0)
                {
                    foreach (Service x in first)
                        if (x.Employee.TypeOfService.Price >= curMinPrice)
                            second.Add(x);

                    first = second;
                    second = new List<Service>();
                }
                if (curMaxPrice > 0)
                {
                    foreach (Service x in first)
                        if (x.Employee.TypeOfService.Price <= curMaxPrice)
                            second.Add(x);

                    first = second;
                }

                foreach (Service x in first)
                    res.Add(x.Id);

                ViewData["YourServices"] = (from a in db.ServiceSet where (res.Any(b => b == a.Id)) select a).ToList();
            }
            else ViewData["YourServices"] = (from a in db.ServiceSet where (a.Client.Id == c.Id) select a).ToArray();
            ViewData["YourVisits"] = (from a in db.VisitSet where (a.Client.Id == c.Id) select a).ToArray();            
            ViewData["TypeOfRooms"] = new SelectList(dataManager.TypeOfRooms.TypeOfRooms(), "Id", "Type");
            ViewData["TypeOfServices"] = new SelectList(dataManager.TypeOfServices.TypeOfServices(), "Id", "Name");
            ViewData["TypeOfPrices"] = new SelectList(dataManager.TypeOfPrices.TypeOfPrices(), "Id", "Type");

            return View();
        }

    }
}