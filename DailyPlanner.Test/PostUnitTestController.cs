using DailyPlanner.API.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Models;
using Repository.Models.DataManager;
using Xunit;

namespace DailyPlanner.Test
{
    public class PostUnitTestController
    {
        private EventManager eventRepository;
        private UserManager userRepository;
        public static DbContextOptions<PlannerDBContext> dbContextOptions { get; }
        public static string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=TestPlannerDB;Trusted_Connection=True;";

        static PostUnitTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<PlannerDBContext>()
                .UseSqlServer(connectionString)
                .Options;
        }
        public PostUnitTestController()
        {
            var context = new PlannerDBContext(dbContextOptions);
            DummyDataDBInitializer db = new DummyDataDBInitializer();
            db.Seed(context);
            userRepository = new UserManager(context);
            eventRepository = new EventManager(context);

        }
        #region GetAll
        [Fact]
        public void GetAll_Return_OkResult()
        {
            //Arrange  
            var controller = new UserController(userRepository);
            //Act  
            var data = controller.GetAll();
            //Assert  
            Assert.IsType<OkObjectResult>(data);
        }
        [Fact]
        public void GetAll_Return_NotFoundResult()
        {
            //Arrange  
            var controller = new UserController(userRepository);
            //Act  
            var data = controller.GetAll();
            //Assert  
            Assert.IsType<NotFoundResult>(data);
        }
        [Fact]
        public void GetAll_Return_BadRequestResult()
        {
            //Arrange  
            var controller = new UserController(userRepository);
            //Act  
            var data = controller.GetAll();
            //Assert  
            Assert.IsType<BadRequestResult>(data);
        }
        [Fact]
        public void GetAll_Return_MatchResult()
        {
            //Arrange  
            var controller = new UserController(userRepository);
            //Act  
            var data = controller.GetAll();
            //Assert  
            Assert.IsType<OkObjectResult>(data);
            var okResult = data.Should().BeOfType<OkObjectResult>().Subject;
            var ev = okResult.Value.Should().BeAssignableTo<User>().Subject;

            Assert.Equal("Test Name 1", ev.FirstName);
            Assert.Equal("Test Email 1", ev.Email);
        }
        #endregion
    }
}
