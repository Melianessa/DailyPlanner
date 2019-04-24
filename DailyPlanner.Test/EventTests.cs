using DailyPlanner.API.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using System;
using System.Globalization;
using System.Linq;
using DailyPlanner.DomainClasses.Enums;
using DailyPlanner.DomainClasses.Models;
using DailyPlanner.Repository;


namespace DailyPlanner.Test
{
    [TestClass]
    public class EventTests
    {
        readonly Event[] _events = new[]
            {
             new Event() { Id=Guid.NewGuid(), Title = "Holidays", Description = "my holiday", StartDate = new DateTime(2019, 10, 12, 09, 09, 09), EndDate = new DateTime(2019, 10, 24, 09, 09, 09), Type = EventEnum.Reminder },
             new Event() { Id=Guid.NewGuid(), Title = "Wedding", Description = "my wedding", StartDate = new DateTime(2018, 10, 12, 09, 09, 09), EndDate = new DateTime(2018, 10, 24, 09, 09, 09), Type = EventEnum.Event },
             new Event() { Id=Guid.NewGuid(), Title = "Work", Description = "my work", StartDate = new DateTime(2019, 10, 12, 09, 09, 09), EndDate = new DateTime(2019, 10, 27, 09, 09, 09), Type = EventEnum.Task }
            }.OrderBy(p => p.Title).ToArray();
        private readonly EventRepository _eventRepository;
        public static DbContextOptions<PlannerDbContext> DbContextOptions { get; }
        public static string ConnectionString = "Server=(localdb)\\MSSQLLocalDB;Database=TestPlannerDB;Trusted_Connection=True;";
        static EventTests()
        {
            DbContextOptions = new DbContextOptionsBuilder<PlannerDbContext>()
                .UseSqlServer(ConnectionString)
                .Options;
        }
        public EventTests()
        {
            var context = new PlannerDbContext(DbContextOptions);
            _eventRepository = new EventRepository(context);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.Events.AddRange(_events);
            context.SaveChanges();
        }
        [TestMethod]
        public void GetEventByDate_Test()
        {
            var controller = new EventController(_eventRepository, _eventRepository);
            //Act  
            var data = controller.GetByDate(new DateTime(2019, 10, 12, 09, 09, 09).Date.ToString(CultureInfo.InvariantCulture)).OrderBy(p => p.Title).ToList();
            //Assert  
            Assert.AreEqual(data[0].Title, _events[0].Title);
        }
        [TestMethod]
        public void GetEvent_Test()
        {
            var controller = new EventController(_eventRepository, _eventRepository);
            //Act  
            var data = controller.Get(_events[0].Id);
            //Assert  
            Assert.AreEqual(data.Title, _events[0].Title);
        }
        [TestMethod]
        public void AddEvent_Test()
        {
            var controller = new EventController(_eventRepository, _eventRepository);
            var newEvent = new Event() { Id = Guid.NewGuid(), Title = "Concert", Description = "my concert", StartDate = new DateTime(2019, 11, 12, 09, 09, 09), EndDate = new DateTime(2019, 10, 13, 09, 09, 09), Type = EventEnum.Reminder };
            //Act  
            var data = controller.Post(newEvent);
            //Assert  
            Assert.AreEqual(data, newEvent.Id);
        }
        [TestMethod]
        public void UpdateEvent_Test()
        {
            var controller = new EventController(_eventRepository, _eventRepository);
            Event newEvent = new Event() { Title = "Concert"};
            var data = controller.Get(_events[0].Id);
            //Act  
            var result = controller.Put(data.Id,newEvent);
            //Assert  
            Assert.AreEqual(result.Title, newEvent.Title);
        }
        [TestMethod]
        public void DeleteEvent_Test()
        {
            var controller = new EventController(_eventRepository, _eventRepository);
            var firstLength = _events.Length;
            //Act
            var result = controller.Delete(_events[0]);
            //Assert
            Assert.AreNotEqual(firstLength,result);
        }
    }
}
