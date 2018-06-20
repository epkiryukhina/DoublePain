using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoublePain.Models
{
    public class JobRepository
    {
        private ModelllContainer db;

        public JobRepository(ModelllContainer cont)
        {
            db = cont;
        }

        public IEnumerable<Job> Jobs()
        {
            return db.JobSet.OrderBy(c => c.JobName);
        }

        public Job GetJob(int id)
        {
            return db.JobSet.Find(id);
        }

        public Job Add(string name)
        {
            Job currentJob = new Job { JobName = name };
            db.JobSet.Add(currentJob);
            db.SaveChanges();

            return currentJob;
        }

        public void Delete(int id)
        {
            Job currentJob = GetJob(id);
            if (currentJob!= null)
            {
                List<Employee> e = currentJob.Employee.ToList();
                foreach (Employee curE in e)
                    db.EmployeeSet.Remove(curE);

                db.JobSet.Remove(currentJob);
                db.SaveChanges();
            }
        }

        public void Edit(int id, string name)
        {
           Job currentJob = GetJob(id);
            if (currentJob != null)
            {
                currentJob.JobName = name;
                db.SaveChanges();
            }
        }
    }
}