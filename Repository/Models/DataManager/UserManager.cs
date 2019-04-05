using System;
using System.Collections.Generic;
using System.Linq;
using Repository.Interfaces;

namespace Repository.Models.DataManager
{
    public class UserManager : IDataRepository<User>
    {
        private readonly PlannerDBContext _context;

        public UserManager(PlannerDBContext context)
        {
            _context = context;
        }
        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User Get(Guid id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            return user;
        }

        public void Add(User b)
        {
            _context.Users.Add(b);
            _context.SaveChanges();
        }

        public void Update(Guid id, User b)
        {
            var user = _context.Users.Find(id);
            if (user!=null)
            {
                user.FirstName = b.FirstName;
                user.LastName = b.LastName;
                user.DateOfBirth = b.DateOfBirth;
                user.Email = b.Email;
                user.Phone = b.Phone;
                user.Role = b.Role;
                user.IsActive = b.IsActive;
                _context.SaveChanges();
            }
        }

        public void Delete(User b)
        {
            if (b != null)
            {
                _context.Users.Remove(b);
                _context.SaveChanges();
            }
        }
    }
}
