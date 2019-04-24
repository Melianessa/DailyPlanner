using System;
using System.Collections.Generic;
using System.Linq;
using DailyPlanner.DomainClasses.Interfaces;
using DailyPlanner.DomainClasses.Models;
using DailyPlanner.Repository;

namespace Repository
{
    public class EventRepository : IDataRepository<Event>, IEventBase<Event>
    {
        private readonly PlannerDbContext _context;

        public EventRepository(PlannerDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Event> GetAll()
        {
            return _context.Events.ToList();
        }
        public IEnumerable<Event> GetByDate(string date)
        {
            var d = DateTime.Parse(date);
            //if (date == null)
            //{
            //    d = DateTime.UtcNow;
            //}
            return _context.Events.Where(p => p.StartDate.Date == d.Date).ToList();
        }

        public Event Get(Guid id)
        {
            var ev = _context.Events.FirstOrDefault(u => u.Id == id);
            return ev;
        }

        public Guid Add(Event b)
        {
            _context.Events.Add(b);
            _context.SaveChanges();
            return b.Id;
        }

        public Event Update(Guid id, Event b)
        {
            var ev = _context.Events.Find(id);
            if (ev != null)
            {
                ev.Title = b.Title;
                ev.Description = b.Description;
                ev.Type = b.Type;
                ev.StartDate = b.StartDate;
                ev.EndDate = b.EndDate;
                ev.IsActive = b.IsActive;
            }
            _context.SaveChanges();
            return b;
        }

        public int Delete(Event b)
        {
            if (b != null)
            {
                _context.Events.Remove(b);
            }
            _context.SaveChanges();
            return _context.Events.Count();
        }
    }
}
