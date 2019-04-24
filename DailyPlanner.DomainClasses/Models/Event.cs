using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DailyPlanner.DomainClasses.Enums;
using DailyPlanner.DomainClasses.Interfaces;

namespace DailyPlanner.DomainClasses.Models
{
    public class Event : IBase
    {
        public Event()
        {
            Id=Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
            IsActive = true;
        }
        [Key]
        public Guid Id { get; set; }
        [Column(TypeName = "varchar(20)")]
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsActive { get; set; }
        public EventEnum Type { get; set; }
        public User User { get; set; }
    }
}
