using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoublePain.Models
{
    public enum Sexes
    {
        Мужчина,
        Женщина
    }

    public class ClientRepository
    {
        private ModelllContainer db;

        public ClientRepository(ModelllContainer cont)
        {
            db = cont;
        }

        public IEnumerable<Client> Clients()
        {
            return db.ClientSet.OrderBy(c => c.Name);
        }

        public Client GetClient(int id)
        {
            return db.ClientSet.Find(id);
        }

        public Client Add(string name, string date, string sex, string passport, string password)
        {
            Client currentClient = new Client { Name = name, DateBirth = DateTime.Parse(date), Sex = sex, Passport = passport, Password = password };
            db.ClientSet.Add(currentClient);
            db.SaveChanges();

            return currentClient;
        }

        public void Delete(int id)
        {
            Client currentClient = GetClient(id);
            if (currentClient != null)
            {
                List<Visit> v = currentClient.Visit.ToList();
                foreach (Visit curV in v)
                {
                    Room r = curV.Room;
                    r.Visit.Remove(curV);
                    db.VisitSet.Remove(curV);
                }

                List<Service> s = currentClient.Service.ToList();
                foreach (Service curS in s)
                {
                    Employee e = curS.Employee;
                    e.Service.Remove(curS);
                    db.ServiceSet.Remove(curS);
                }

                db.ClientSet.Remove(currentClient);
                db.SaveChanges();
            }
        }

        public void Edit(int id, string name, string date, string sex, string passport, string password)
        {
            Client currentClient = GetClient(id);
            if (currentClient != null)
            {
                currentClient.Name = name;
                currentClient.DateBirth = DateTime.Parse(date);
                currentClient.Sex = sex;
                currentClient.Passport = passport;
                currentClient.Password = password;
                db.SaveChanges();
            }
        }
    }
}