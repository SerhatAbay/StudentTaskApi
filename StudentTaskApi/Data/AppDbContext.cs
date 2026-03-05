using Microsoft.EntityFrameworkCore;
using StudentTaskApi.Models;

namespace StudentTaskApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students => Set<Student>();
        public DbSet<StudentTask> StudentTasks => Set<StudentTask>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentTask>()
                .HasOne(st => st.Student)
                .WithMany(s => s.StudentTasks)
                .HasForeignKey(st => st.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
