using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using TaskEntity = Domain.Entities.Task;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<TaskEntity> Tasks { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(u => u.Username).HasMaxLength(100);
                entity.Property(u => u.Email).HasMaxLength(256);
                entity.Property(u => u.Password).HasMaxLength(256);
            });

            modelBuilder.Entity<TaskEntity>(entity =>
            {
                entity.Property(t => t.Title).HasMaxLength(200);
                entity.Property(t => t.Description).HasMaxLength(2000);
            });
        }
    }
}
