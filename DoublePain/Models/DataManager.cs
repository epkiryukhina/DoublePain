using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoublePain.Models
{
    public class DataManager
    {
        private ModelllContainer db;
        public ClientRepository Clients;
        public EmployeeRepository Employees;
        public JobRepository Jobs;
        public TypeOfPriceRepository TypeOfPrices;
        public TypeOfServiceRepository TypeOfServices;
        public ServiceRepository Services;
        public VisitRepository Visits;
        public RoomRepository Rooms;
        public TypeOfRoomRepository TypeOfRooms;

        public DataManager()
        {
            db = new ModelllContainer();
            Clients = new ClientRepository(db);
            Employees = new EmployeeRepository(db);
            Jobs = new JobRepository(db);
            TypeOfPrices = new TypeOfPriceRepository(db);
            TypeOfServices = new TypeOfServiceRepository(db);
            Services = new ServiceRepository(db);
            Visits = new VisitRepository(db);
            Rooms = new RoomRepository(db);
            TypeOfRooms = new TypeOfRoomRepository(db);
        }
    }
}