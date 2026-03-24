using Microsoft.EntityFrameworkCore;
using evoNaplo.Models;

namespace evoNaplo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Mentor> Mentors { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<ProjectLink> ProjectLink { get; set; }
    }
}