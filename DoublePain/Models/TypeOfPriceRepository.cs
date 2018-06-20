using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoublePain.Models
{
    public class TypeOfPriceRepository
    {
        private ModelllContainer db;

        public TypeOfPriceRepository(ModelllContainer cont)
        {
            db = cont;
        }

        public IEnumerable<TypeOfPrice> TypeOfPrices()
        {
            return db.TypeOfPriceSet.OrderBy(c => c.Type);
        }

        public TypeOfPrice GetTypeOfPrice(int id)
        {
            return db.TypeOfPriceSet.Find(id);
        }

        public TypeOfPrice Add(string type)
        {
            TypeOfPrice currentTypeOfPrice = new TypeOfPrice { Type = type };
            db.TypeOfPriceSet.Add(currentTypeOfPrice);
            db.SaveChanges();

            return currentTypeOfPrice;
        }

        public void Delete(int id)
        {
            TypeOfPrice currentTypeOfPrice = GetTypeOfPrice(id);
            if (currentTypeOfPrice != null)
            {
                List<TypeOfService> ts = currentTypeOfPrice.TypeOfService.ToList();
                foreach (TypeOfService curTS in ts)
                {
                    List<Employee> e = curTS.Employee.ToList();
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
                    db.TypeOfServiceSet.Remove(curTS);
                }

                db.TypeOfPriceSet.Remove(currentTypeOfPrice);
                db.SaveChanges();
            }
        }

        public void Edit(int id, string type)
        {
            TypeOfPrice currentTypeOfPrice = GetTypeOfPrice(id);
            if (currentTypeOfPrice != null)
            {
                currentTypeOfPrice.Type = type;
                db.SaveChanges();
            }
        }
    }
}