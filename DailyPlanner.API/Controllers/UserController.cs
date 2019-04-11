using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Interfaces;
using Repository.Models;

namespace DailyPlanner.API.Controllers
{
    [Route("api/[controller]")]

    public class UserController : ControllerBase
    {
        // GET api/values
        private readonly IDataRepository<User> _iRepo;

        public UserController(IDataRepository<User> repo)
        {
            _iRepo = repo;
        }
        [HttpGet]
        public IEnumerable<User> GetAll()
        {
            return _iRepo.GetAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public User Get(Guid id)
        {
            return _iRepo.Get(id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] User user)
        {
            _iRepo.Add(user);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] User user)
        {
            _iRepo.Update(id,user);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(User b)
        {
            _iRepo.Delete(b);
        }
    }
}
