using System;
using System.Collections.Generic;
using System.Linq;
using Repository.Interfaces;


namespace Repository.Models.DataManager
{
    public class EventManager : IDataRepository<Event>
    {
        private readonly PlannerDBContext _context;

        public EventManager(PlannerDBContext context)
        {
            _context = context;
        }
        public IEnumerable<Event> GetAll()
        {
            return _context.Events.ToList();
        }

        public Event Get(Guid id)
        {
            var ev = _context.Events.FirstOrDefault(u => u.Id == id);
            return ev;
        }

        public void Add(Event b)
        {
            _context.Events.Add(b);
            _context.SaveChanges();
        }

        public void Update(Guid id, Event b)
        {
            var ev = _context.Events.Find(id);
            if (ev!=null)
            {
                ev.Title = b.Title;
                ev.Description = b.Description;
                ev.Type = b.Type;
                ev.StartDate = b.StartDate;
                ev.EndDate = b.EndDate;
                ev.IsActive = b.IsActive;
            }
        }

        public void Delete(Event b)
        {
            if (b != null)
            {
                _context.Events.Remove(b);
                _context.SaveChanges();
            }
        }
    }
}
