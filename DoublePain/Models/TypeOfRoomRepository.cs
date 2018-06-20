using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoublePain.Models
{
    public class TypeOfRoomRepository
    {
        private ModelllContainer db;

        public TypeOfRoomRepository(ModelllContainer cont)
        {
            db = cont;
        }

        public IEnumerable<TypeOfRoom> TypeOfRooms()
        {
            return db.TypeOfRoomSet.OrderBy(c => c.Type);
        }

        public TypeOfRoom GetTypeOfRoom(int id)
        {
            return db.TypeOfRoomSet.Find(id);
        }

        public TypeOfRoom Add(string type, int price, int capacity)
        {
            TypeOfRoom currentTypeOfRoom = new TypeOfRoom { Type = type, Price = price, Capacity = capacity };
            db.TypeOfRoomSet.Add(currentTypeOfRoom);
            db.SaveChanges();

            return currentTypeOfRoom;
        }

        public void Delete(int id)
        {
            TypeOfRoom currentTypeOfRoom = GetTypeOfRoom(id);
            if (currentTypeOfRoom != null)
            {
                List<Room> r = currentTypeOfRoom.Room.ToList();
                foreach (Room curR in r)
                {
                    List<Visit> v = curR.Visit.ToList();
                    foreach (Visit curV in v)
                    {
                        Client c = curV.Client;
                        c.Visit.Remove(curV);
                        db.VisitSet.Remove(curV);
                    }
                    db.RoomSet.Remove(curR);
                }

                db.TypeOfRoomSet.Remove(currentTypeOfRoom);
                db.SaveChanges();
            }
        }

        public void Edit(int id, string type, int price, int capacity)
        {
            TypeOfRoom currentTypeOfRoom = GetTypeOfRoom(id);
            if (currentTypeOfRoom != null)
            {
                currentTypeOfRoom.Type = type;
                currentTypeOfRoom.Price = price;
                currentTypeOfRoom.Capacity = capacity;
                db.SaveChanges();
            }
        }
    }
}