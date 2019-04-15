using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Repository.Interfaces;

namespace Repository.Models
{
    public class User:IBase
    {
        public User()
        {
            Id=Guid.NewGuid();
            CreationDate = DateTime.Now.Ticks;
            IsActive = true;
        }
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long CreationDate { get; set; }
        public long DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool Sex { get; set; }
        public bool IsActive { get; set; }
        public RoleEnum Role { get; set; }
        public ICollection<Event> Events { get; set; }
    }
}
