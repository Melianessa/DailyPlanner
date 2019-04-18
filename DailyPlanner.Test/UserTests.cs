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
                new User() { Id = Guid.NewGuid(), FirstName = "Alex", LastName = "Brown", DateOfBirth = new DateTime(1996, 08, 12), Email = "al@r.ua", Phone = "3333333", Role = RoleEnum.Admin, Sex = true },
                new User() { Id = Guid.NewGuid(), FirstName = "Karl", LastName = "Gop", DateOfBirth = new DateTime(1992, 01, 11), Email = "al3@r1.ua", Phone = "1111111", Role = RoleEnum.Client, Sex = false },
                new User() { Id = Guid.NewGuid(), FirstName = "Kate", LastName = "Lissa", DateOfBirth = new DateTime(1991, 10, 12), Email = "al1@r.ua", Phone = "2222222", Role = RoleEnum.Admin, Sex = false }
        }.OrderBy(p => p.FirstName).ToArray();

        private readonly UserManager _userRepository;
        public static DbContextOptions<PlannerDBContext> DbContextOptions { get; }
        public static string ConnectionString = "Server=(localdb)\\MSSQLLocalDB;Database=TestPlannerDB;Trusted_Connection=True;";
        static UserTests()
        {
            DbContextOptions = new DbContextOptionsBuilder<PlannerDBContext>()
                .UseSqlServer(ConnectionString)
                .Options;
        }
        public UserTests()
        {
            var context = new PlannerDBContext(DbContextOptions);
            _userRepository = new UserManager(context);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.Users.AddRange(users);
            context.SaveChanges();
        }
        [TestMethod]
        public void GetAllUsers_Test()
        {
            var controller = new UserController(_userRepository);
            //Act  
            var data = controller.GetAll().OrderBy(p => p.FirstName).ToList();
            //Assert  
            Assert.AreEqual(data[0].FirstName, users[0].FirstName);
        }
        [TestMethod]
        public void AddUser_Test()
        {
            var controller = new UserController(_userRepository);
            User newUser = new User() { Id = Guid.NewGuid(), FirstName = "Mary", LastName = "Lane", DateOfBirth = new DateTime(1991, 08, 12), Email = "mary@r.ua", Phone = "7777777", Role = RoleEnum.Admin, Sex = false };
            //Act  
            var data = controller.Post(newUser);
            //Assert  
            Assert.AreEqual(data, newUser.Id);
        }
        [TestMethod]
        public void GetUser_Test()
        {
            var controller = new UserController(_userRepository);
            //Act  
            var data = controller.Get(users[0].Id);
            //Assert  
            Assert.AreEqual(data.Email, users[0].Email);
        }
        [TestMethod]
        public void UpdateUser_Test()
        {
            var controller = new UserController(_userRepository);
            User newUser = new User() { LastName = "Snow" };
            var data = controller.Get(users[0].Id);
            //Act  
            var result = controller.Put(data.Id, newUser);
            //Assert  
            Assert.AreEqual(result.LastName, newUser.LastName);
        }
        [TestMethod]
        public void DeleteUser_Test()
        {
            var controller = new UserController(_userRepository);
            var firstLength = users.Length;
            //Act
            var result = controller.Delete(users[0]);
            //Assert
            Assert.AreNotEqual(firstLength, result);
        }
    };
}
