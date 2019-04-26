using System;
using System.Collections.Generic;
using DailyPlanner.DomainClasses;
using Microsoft.AspNetCore.Mvc;
using DailyPlanner.DomainClasses.Interfaces;
using DailyPlanner.DomainClasses.Models;

namespace DailyPlanner.API.Controllers
{
    [Route("api/[controller]/[action]")]

    public class UserController : ControllerBase
    {
        // GET api/values
        private readonly IUserRepository _iRepo;

        public UserController(IUserRepository repo)
        {
            _iRepo = repo;
        }
        [HttpGet]
        public IEnumerable<User> GetAll()
        {
            return _iRepo.GetAll();
        }
        [HttpGet]
        public IEnumerable<UserDTO> GetAllUsers()
        {
            return _iRepo.GetAllUsers();
        }
        // GET api/values/5
        //[HttpGet("{id}")]
        //public User Get(Guid id)
        //{
        //    return _iRepo.Get(id);
        //}
        [HttpGet("{id}")]
        public UserDTO GetUser(Guid id)
        {
            return _iRepo.GetUser(id);
        }

        // POST api/values
        [HttpPost]
        public Guid Post([FromBody] User user)
        {
            return _iRepo.Add(user);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public User Put([FromBody] User user)
        {
            return _iRepo.Update(user);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public int Delete(User b)
        {
            return _iRepo.Delete(b);
        }
    }
}
