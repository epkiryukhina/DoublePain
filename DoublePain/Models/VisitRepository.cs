using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoublePain.Models
{
    public class VisitRepository
    {
        private ModelllContainer db;

        public VisitRepository(ModelllContainer cont)
        {
            db = cont;
        }

        public IEnumerable<Visit> Visits()
        {
            return db.VisitSet.OrderBy(c => c.Client.Name);
        }

        public Visit GetVisit(int id)
        {
            return db.VisitSet.Find(id);
        }

        public Visit Add(int client, int room, string firstDate, string secondDate)
        {
            Visit currentVisit = new Visit { Client = db.ClientSet.Find(client), Room = db.RoomSet.Find(room), FirstDate = DateTime.Parse(firstDate), SecondDate = DateTime.Parse(secondDate) };
            db.VisitSet.Add(currentVisit);
            db.SaveChanges();

            return currentVisit;
        }

        public void Delete(int id)
        {
            Visit currentVisit = GetVisit(id);
            if (currentVisit != null)
            {
                Room r = currentVisit.Room;
                r.Visit.Remove(currentVisit);

                Client c = currentVisit.Client;
                c.Visit.Remove(currentVisit);

                db.VisitSet.Remove(currentVisit);
                db.SaveChanges();
            }
        }

        public void Edit(int id, Client client, Room room, DateTime firstDate, DateTime secondDate)
        {
            Visit currentVisit = GetVisit(id);
            if (currentVisit != null)
            {
                currentVisit.Client = client;
                currentVisit.Room = room;
                currentVisit.FirstDate = firstDate;
                currentVisit.SecondDate = secondDate;
                db.SaveChanges();
            }
        }
    }
}