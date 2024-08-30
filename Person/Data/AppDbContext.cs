using Microsoft.EntityFrameworkCore;
using Person.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Person.Data
{
    namespace Person.Data
    {
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions options) : base(options) { }

            public DbSet<Persone> Persons { get; set; }
            public DbSet<Project> Projects { get; set; }
            public DbSet<PersonProject> PersonProjects { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<PersonProject>()
                    .HasKey(pp => new { pp.PersonId, pp.ProjectId });

                modelBuilder.Entity<PersonProject>()
                    .HasOne(pp => pp.Person)
                    .WithMany(p => p.PersonProjects)
                    .HasForeignKey(pp => pp.PersonId);

                modelBuilder.Entity<PersonProject>()
                    .HasOne(pp => pp.Project)
                    .WithMany(p => p.PersonProjects)
                    .HasForeignKey(pp => pp.ProjectId);
            }
        }
    }

}
