using System;
using DailyPlanner.DomainClasses.Enums;
using DailyPlanner.DomainClasses.Models;

namespace DailyPlanner.DomainClasses
{
    public class UserDTO
    {
        public UserDTO(User user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            CreationDate = user.CreationDate;
            DateOfBirth = user.DateOfBirth;
            Phone = user.Phone;
            Email = user.Email;
            Sex = user.Sex;
            IsActive = user.IsActive;
            Role = user.Role;
            EventCount = user.Events?.Count ?? 0;
        }
        public UserDTO() { }

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
        public int EventCount { get; set; }
    }
}
