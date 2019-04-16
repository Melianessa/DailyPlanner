using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Repository.Interfaces;

namespace Repository.Models
{
    public class User:IBase
    {
        public User()
        {
            Id=Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
            IsActive = true;
        }
        [Key]
        public Guid Id { get; set; }
        [Column(TypeName = "varchar(20)")]
        [Required]
        public string FirstName { get; set; }
        [Column(TypeName = "varchar(40)")]
        [Required]
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
