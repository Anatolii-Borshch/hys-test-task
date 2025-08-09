using Microsoft.EntityFrameworkCore;
using ScheduleMeetingSystem.Core.Models;

namespace ScheduleMeetingSystem.Infrastructure.DbContext
{
    public class ScheduleMeetingSystemDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public ScheduleMeetingSystemDbContext(DbContextOptions<ScheduleMeetingSystemDbContext> options): base(options){}
        
        public DbSet<User> Users { get; set; }
        public DbSet<Meeting> Meetings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();
            
            modelBuilder.Entity<Meeting>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();
            
            modelBuilder.Entity<User>()
                .HasMany(x => x.Meetings)
                .WithMany(x => x.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserMeetings",
                    x => x
                        .HasOne<Meeting>()
                        .WithMany()
                        .HasForeignKey("MeetingId")
                        .OnDelete(DeleteBehavior.Cascade),
                    x => x
                        .HasOne<User>()
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                )
                .HasKey("UserId", "MeetingId");
        }
    }
}