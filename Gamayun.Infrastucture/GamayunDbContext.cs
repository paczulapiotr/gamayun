using Gamayun.Identity;
using Gamayun.Infrastucture.Entities;
using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gamayun.Infrastucture
{
    public class GamayunDbContext : IdentityDbContext<AppUser>
    {
        public GamayunDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<PresenceDate> PresenceDates { get; set; }
        public DbSet<Presence> Presences { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentSection> StudentSections { get; set; }
        public DbSet<Teacher> Teachers{ get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new StudentSectionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PresenceDateEntityTypeConfiguration());
        }
    }
}
