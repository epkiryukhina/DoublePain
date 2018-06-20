using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoublePain.Models
{
    public class ServiceRepository
    {
        private ModelllContainer db;

        public ServiceRepository(ModelllContainer cont)
        {
            db = cont;
        }

        public IEnumerable<Service> Services()
        {
            return db.ServiceSet.OrderBy(c => c.Date);
        }

        public Service GetService(int id)
        {
            return db.ServiceSet.Find(id);
        }

        public Service Add(int client, int employee, string date, int numberOfHours, int numberOfPeople)
        {
            Service currentService = new Service { Client = db.ClientSet.Find(client), Employee = db.EmployeeSet.Find(employee), Date = DateTime.Parse(date), NumberOfHours = numberOfHours, NumberOfPeople = numberOfPeople };
            db.ServiceSet.Add(currentService);
            db.SaveChanges();

            return currentService;
        }

        public void Delete(int id)
        {
            Service currentService = GetService(id);
            if (currentService != null)
            {
                Client c = currentService.Client;
                c.Service.Remove(currentService);

                Employee e = currentService.Employee;
                e.Service.Remove(currentService);

                db.ServiceSet.Remove(currentService);
                db.SaveChanges();
            }
        }

        public void Edit(int id, Client client, Employee employee, DateTime date, int numberOfHours, int numberOfPeople)
        {
            Service currentService = GetService(id);
            if (currentService != null)
            {
                currentService.Client = client;
                currentService.Employee = employee;
                currentService.Date = date;
                currentService.NumberOfHours = numberOfHours;
                currentService.NumberOfPeople = numberOfPeople;
                db.SaveChanges();
            }
        }
    }
}