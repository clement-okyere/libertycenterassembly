using LibertyFamilySystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibertyFamilySystem.Data
{
    public class EventsDbContext: DbContext
    {
        public EventsDbContext(DbContextOptions<EventsDbContext> options)
           : base(options)
        {
        }
        public DbSet<Event> Event { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>()
                .Property(p => p.IsActive)
                .HasDefaultValue(false);
        }
    }
}
