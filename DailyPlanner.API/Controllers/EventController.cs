using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Interfaces;

namespace DailyPlanner.API.Controllers
{
    [Route("api/[controller]")]
    
    public class EventController : ControllerBase
    {
        private readonly IDataRepository<Event> _iRepo;

        public EventController(IDataRepository<Event> repo)
        {
            _iRepo = repo;
        }
        [HttpGet]
        public IEnumerable<Event> GetAll()
        {
            return _iRepo.GetAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Event Get(Guid id)
        {
            return _iRepo.Get(id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] Event ev)
        {
            _iRepo.Add(ev);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] Event ev)
        {
            _iRepo.Update(id, ev);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(Event ev)
        {
            _iRepo.Delete(ev);
        }
    }
}