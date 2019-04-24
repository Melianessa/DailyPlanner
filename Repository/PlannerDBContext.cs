using DailyPlanner.DomainClasses.Models;
using Microsoft.EntityFrameworkCore;

namespace DailyPlanner.Repository
{
    public class PlannerDbContext:DbContext
    {
        public PlannerDbContext(DbContextOptions opt):base(opt)
        { }
        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
