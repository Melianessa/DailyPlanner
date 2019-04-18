//using Repository;
//using Repository.Models;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace DailyPlanner.Test
//{
//    public class DummyDataDBInitializer
//    {
//        public DummyDataDBInitializer()
//        { }
//        public void Seed(PlannerDBContext context)
//        {
//            context.Database.EnsureDeleted();
//            context.Database.EnsureCreated();
//            context.Users.AddRange(
//                new User() { FirstName = "Alex", LastName = "Brown", DateOfBirth = new DateTime(1996, 08, 12), Email = "al@r.ua", Phone = "3333333", Role = RoleEnum.Admin, Sex = true },
//                new User() { FirstName = "Karl", LastName = "Gop", DateOfBirth = new DateTime(1992, 01, 11), Email = "al3@r1.ua", Phone = "1111111", Role = RoleEnum.Client, Sex = false },
//                new User() { FirstName = "Kate", LastName = "Lissa", DateOfBirth = new DateTime(1991, 10, 12), Email = "al1@r.ua", Phone = "2222222", Role = RoleEnum.Admin, Sex = false });
//            context.Events.AddRange(
//                new Event() { Title = "Holidays", Description = "my holiday", StartDate = new DateTime(2019, 10, 12, 09, 09, 09), EndDate = new DateTime(2019, 10, 24, 09, 09, 09), Type = EventEnum.Reminder },
//             new Event() { Title = "Wedding", Description = "my wedding", StartDate = new DateTime(2018, 10, 12, 09, 09, 09), EndDate = new DateTime(2018, 10, 24, 09, 09, 09), Type = EventEnum.Event },
//             new Event() { Title = "Work", Description = "my work", StartDate = new DateTime(2019, 10, 12, 09, 09, 09), EndDate = new DateTime(2019, 10, 27, 09, 09, 09), Type = EventEnum.Task });
//            context.SaveChanges();
//        }
//    }
//}
