using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoublePain.Models
{
    public class EmployeeRepository
    {
        private ModelllContainer db;

        public EmployeeRepository(ModelllContainer cont)
        {
            db = cont;
        }

        public IEnumerable<Employee> Employees()
        {
            return db.EmployeeSet.OrderBy(e => e.Name);
        }

        public Employee GetEmployee(int id)
        {
            return db.EmployeeSet.Find(id);
        }

        public Employee Add(string name, string date, string sex, string passport, string password, int job, int typeOfService)
        {
            Employee currentEmployee = new Employee { Name = name, DateBirth = DateTime.Parse(date), Sex = sex, Passport = passport, Password = password, Job = db.JobSet.Find(job), TypeOfService = db.TypeOfServiceSet.Find(typeOfService) };
            db.EmployeeSet.Add(currentEmployee);
            db.SaveChanges();

            return currentEmployee;
        }

        public void Delete(int id)
        {
            Employee currentEmployee = GetEmployee(id);
            if (currentEmployee != null)
            {
                Job j = currentEmployee.Job;
                j.Employee.Remove(currentEmployee);

                TypeOfService ts = currentEmployee.TypeOfService;
                ts.Employee.Remove(currentEmployee);

                List<Service> s = currentEmployee.Service.ToList();
                foreach (Service curS in s)
                {
                    Client c = curS.Client;
                    c.Service.Remove(curS);
                    db.ServiceSet.Remove(curS);
                }

                db.EmployeeSet.Remove(currentEmployee);
                db.SaveChanges();
            }
        }

        public void Edit(int id, string name, string date, string sex, string passport, string password, int job, int typeOfService)
        {
            Employee currentEmployee = GetEmployee(id);
            if (currentEmployee != null)
            {
                currentEmployee.Name = name;
                currentEmployee.DateBirth = DateTime.Parse(date);
                currentEmployee.Sex = sex;
                currentEmployee.Passport = passport;
                currentEmployee.Password = password;
                currentEmployee.Job = db.JobSet.Find(job);
                currentEmployee.TypeOfService = db.TypeOfServiceSet.Find(typeOfService);
                db.SaveChanges();
            }
        }
    }
}