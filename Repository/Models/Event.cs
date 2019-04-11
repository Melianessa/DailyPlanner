using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Repository.Interfaces;
using Repository.Models;

namespace Repository
{
    public class Event : IBase
    {
        public Event()
        {
            Id=Guid.NewGuid();
            CreationDate=DateTime.UtcNow;
            IsActive = true;
        }
        [Key]
        public Guid Id { get; set; }
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
