using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoublePain.Models
{
    public class RoomRepository
    {
        private ModelllContainer db;

        public RoomRepository(ModelllContainer cont)
        {
            db = cont;
        }

        public IEnumerable<Room> Rooms()
        {
            return db.RoomSet.OrderBy(c => c.Number);
        }

        public Room GetRoom(int id)
        {
            return db.RoomSet.Find(id);
        }

        public Room Add(string number, int typeOfRoom)
        {
            Room currentRoom = new Room { Number = number, TypeOfRoom = db.TypeOfRoomSet.Find(typeOfRoom)};
            db.RoomSet.Add(currentRoom);
            db.SaveChanges();

            return currentRoom;
        }

        public void Delete(int id)
        {
            Room currentRoom = GetRoom(id);
            if (currentRoom != null)
            {
                TypeOfRoom tr = currentRoom.TypeOfRoom;
                tr.Room.Remove(currentRoom);

                List<Visit> v = currentRoom.Visit.ToList();
                foreach(Visit curV in v)
                {
                    Client c = curV.Client;
                    c.Visit.Remove(curV);
                    db.VisitSet.Remove(curV);
                }

                db.RoomSet.Remove(currentRoom);
                db.SaveChanges();
            }
        }

        public void Edit(int id, string number, TypeOfRoom typeOfRoom)
        {
            Room currentRoom = GetRoom(id);
            if (currentRoom != null)
            {
                currentRoom.Number = number;
                currentRoom.TypeOfRoom = typeOfRoom;
                db.SaveChanges();
            }
        }
    }
}