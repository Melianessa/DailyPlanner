using System;
using System.Collections.Generic;
using DailyPlanner.DomainClasses.Interfaces;
using DailyPlanner.DomainClasses.Models;
using Microsoft.AspNetCore.Mvc;

namespace DailyPlanner.API.Controllers
{
    [Route("api/[controller]/[action]")]

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
        public IEnumerable<Event> GetByDate([FromBody] string date)
        {
            return _iEventRepo.GetByDate(date);
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
        public Guid Post([FromBody] Event ev)
        {
            return _iRepo.Add(ev);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public Event Put(Guid id, [FromBody] Event ev)
        {
            return _iRepo.Update(id, ev);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public int Delete(Event ev)
        {
            return _iRepo.Delete(ev);
        }
    }
}