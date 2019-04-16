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
        private readonly IEventBase<Event> _iEventRepo;

        public EventController(IDataRepository<Event> repo, IEventBase<Event> evRepo)
        {
            _iRepo = repo;
            _iEventRepo = evRepo;
        }

        [HttpPost]
        public IEnumerable<Event> PostByDate(string date)
        {
            return _iEventRepo.PostByDate(date);
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