using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Repository.Models;

namespace Repository
{
    public class PlannerDBContext:DbContext
    {
        public PlannerDBContext(DbContextOptions opt):base(opt)
        { }
        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
