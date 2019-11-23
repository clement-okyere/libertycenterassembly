using LibertyFamilySystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibertyFamilySystem.Data
{

    public class MembersDbContext:DbContext
    {
    public MembersDbContext(DbContextOptions<MembersDbContext> options)
           : base(options)
    {
    }
     public DbSet<Member> Member { get; set; }
     public DbSet<Occupation> Occupation { get; set; }
     public DbSet<Group> Group { get; set; }
     public DbSet<Event> Event { get; set; }
     public DbSet<Attendance> Attendance { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Member>()
            .Property(p => p.IsGroupAdmin)
            .HasDefaultValue(false);

         modelBuilder.Entity<Event>()
                .Property(p => p.CreatedAt)
                .HasDefaultValue(DateTime.Now);

         modelBuilder.Entity<Attendance>()
                   .Property(p => p.CreatedAt)
                   .HasDefaultValue(DateTime.Now);
        }
    }
}
