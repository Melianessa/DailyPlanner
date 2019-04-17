using DailyPlanner.API.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using Repository.Models;
using Repository.Models.DataManager;
using System;
using System.Linq;


namespace DailyPlanner.Test
{
    [TestClass]
    public class UserTests
    {
        User[] users = new[]
            {
                new User() { FirstName = "Alex", LastName = "Brown", DateOfBirth = new DateTime(1996, 08, 12), Email = "al@r.ua", Phone = "3333333", Role = RoleEnum.Admin, Sex = true },
                new User() { FirstName = "Karl", LastName = "Gop", DateOfBirth = new DateTime(1992, 01, 11), Email = "al3@r1.ua", Phone = "1111111", Role = RoleEnum.Client, Sex = false },
                new User() { FirstName = "Kate", LastName = "Lissa", DateOfBirth = new DateTime(1991, 10, 12), Email = "al1@r.ua", Phone = "2222222", Role = RoleEnum.Admin, Sex = false }
        }.OrderBy(p => p.FirstName).ToArray();

        private UserManager userRepository;
        public static DbContextOptions<PlannerDBContext> dbContextOptions { get; }
        public static string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=TestPlannerDB;Trusted_Connection=True;";
        static UserTests()
        {
            dbContextOptions = new DbContextOptionsBuilder<PlannerDBContext>()
                .UseSqlServer(connectionString)
                .Options;
        }
        public UserTests()
        {
            var context = new PlannerDBContext(dbContextOptions);
            DummyDataDBInitializer db = new DummyDataDBInitializer();
            userRepository = new UserManager(context);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.Users.AddRange(users);
            context.SaveChanges();
        }
        [TestMethod]
        public void GetAll_Test()
        {
            var controller = new UserController(userRepository);
            //Act  
            var data = controller.GetAll().OrderBy(p => p.FirstName).ToList();
            //Assert  
            Assert.AreEqual(data[0].FirstName, users[0].FirstName);
        }
    };
}
