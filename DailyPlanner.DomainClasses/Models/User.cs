using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DailyPlanner.DomainClasses.Enums;
using DailyPlanner.DomainClasses.Interfaces;

namespace DailyPlanner.DomainClasses.Models
{
    public class User:IBase
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool Sex { get; set; }
        public bool IsActive { get; set; }
        public RoleEnum Role { get; set; }
        public ICollection<Event> Events { get; set; }
    }
}
