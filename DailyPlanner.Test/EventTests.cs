using DailyPlanner.API.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using Repository.Interfaces;
using Repository.Models.DataManager;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace DailyPlanner.Test
{
    [TestClass]
    public class EventTests
    {
        Event[] events = new[]
            {
             new Event() { Id=Guid.NewGuid(), Title = "Holidays", Description = "my holiday", StartDate = new DateTime(2019, 10, 12, 09, 09, 09), EndDate = new DateTime(2019, 10, 24, 09, 09, 09), Type = EventEnum.Reminder },
             new Event() { Id=Guid.NewGuid(), Title = "Wedding", Description = "my wedding", StartDate = new DateTime(2018, 10, 12, 09, 09, 09), EndDate = new DateTime(2018, 10, 24, 09, 09, 09), Type = EventEnum.Event },
             new Event() { Id=Guid.NewGuid(), Title = "Work", Description = "my work", StartDate = new DateTime(2019, 10, 12, 09, 09, 09), EndDate = new DateTime(2019, 10, 27, 09, 09, 09), Type = EventEnum.Task }
            }.OrderBy(p => p.Title).ToArray();
        private EventManager eventRepository;
        public static DbContextOptions<PlannerDBContext> dbContextOptions { get; }
        public static string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=TestPlannerDB;Trusted_Connection=True;";
        static EventTests()
        {
            dbContextOptions = new DbContextOptionsBuilder<PlannerDBContext>()
                .UseSqlServer(connectionString)
                .Options;
        }
        public EventTests()
        {
            var context = new PlannerDBContext(dbContextOptions);
            DummyDataDBInitializer db = new DummyDataDBInitializer();
            eventRepository = new EventManager(context);
        }
        [TestMethod]
        public void GetEventByDate_Test()
        {
            var controller = new EventController(eventRepository, eventRepository);
            //Act  
            var data = controller.GetByDate(new DateTime(2019, 10, 12, 09, 09, 09).Date.ToString(CultureInfo.InvariantCulture)).OrderBy(p => p.Title).ToList();
            //Assert  
            Assert.AreEqual(data[0].Title, events[0].Title);
        }
        [TestMethod]
        public void GetEvent_Test()
        {
            var controller = new EventController(eventRepository, eventRepository);
            //Act  
            var data = controller.Get(events[0].Id);
            //Assert  
            Assert.AreEqual(data.Title, events[0].Title);
        }
    }
}
