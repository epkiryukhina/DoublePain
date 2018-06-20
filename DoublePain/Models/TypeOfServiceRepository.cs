using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoublePain.Models
{
    public class TypeOfServiceRepository
    {
        private ModelllContainer db;

        public TypeOfServiceRepository(ModelllContainer cont)
        {
            db = cont;
        }

        public IEnumerable<TypeOfService> TypeOfServices()
        {
            return db.TypeOfServiceSet.OrderBy(c => c.Name);
        }

        public TypeOfService GetTypeOfService(int id)
        {
            return db.TypeOfServiceSet.Find(id);
        }

        public TypeOfService Add(string name, int typeOfPrice, int price)
        {
            TypeOfService currentTypeOfService = new TypeOfService { Name = name, TypeOfPrice = db.TypeOfPriceSet.Find(typeOfPrice), Price = price };
            db.TypeOfServiceSet.Add(currentTypeOfService);
            db.SaveChanges();

            return currentTypeOfService;
        }

        public void Delete(int id)
        {
            DataManager dm = new DataManager();

            TypeOfService currentTypeOfService = GetTypeOfService(id);
            if (currentTypeOfService != null)
            {
                TypeOfPrice tp = currentTypeOfService.TypeOfPrice;
                tp.TypeOfService.Remove(currentTypeOfService);

                List<Employee> e = currentTypeOfService.Employee.ToList();
                foreach (Employee curE in e)
                {
                    List<Service> s = curE.Service.ToList();
                    foreach (Service curS in s)
                    {
                        Client c = curS.Client;
                        c.Service.Remove(curS);
                        db.ServiceSet.Remove(curS);
                    }
                    db.EmployeeSet.Remove(curE);
                }

                db.TypeOfServiceSet.Remove(currentTypeOfService);
                db.SaveChanges();
            }
        }

        public void Edit(int id, string name, TypeOfPrice typeOfPrice, int price)
        {
            TypeOfService currentTypeOfService = GetTypeOfService(id);
            if (currentTypeOfService != null)
            {
                currentTypeOfService.Name = name;
                currentTypeOfService.TypeOfPrice = typeOfPrice;
                currentTypeOfService.Price = price;
                db.SaveChanges();
            }
        }
    }
}