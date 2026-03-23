using Microsoft.EntityFrameworkCore;
using StudentTaskApi.Models;

namespace StudentTaskApi.Data
{
    public class AppDbContext : DbContext // AppDbContext is the class that inherits from DbContext(Entity Framework Core's primary class for interacting with the database)
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students => Set<Student>();
        public DbSet<StudentTask> StudentTasks => Set<StudentTask>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Instructor> Instructors => Set<Instructor>();
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder) // This method is used to configure the model and its relationships
        {
            modelBuilder.Entity<StudentTask>()
                .HasOne(st => st.Student) // Each StudentTask has one Student
                .WithMany(s => s.StudentTasks) // Each Student can have many StudentTasks(one to many relationship)
                .HasForeignKey(st => st.StudentId) // The foreign key in StudentTask that references the Student entity
                .OnDelete(DeleteBehavior.Cascade); // When a Student is deleted, all related StudentTasks will also be deleted (cascade delete)

            modelBuilder.Entity<StudentTask>()
                .HasOne(st => st.Course)
                .WithMany(c => c.StudentTasks)
                .HasForeignKey(st => st.CourseId)
                .OnDelete(DeleteBehavior.SetNull); //SetNull because Course should still exist, so only set to null

            modelBuilder.Entity<StudentTask>()
                .HasOne(st => st.Category)
                .WithMany(c => c.StudentTasks)
                .HasForeignKey(st => st.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Instructor)
                .WithMany(i => i.Courses)
                .HasForeignKey(c => c.InstructorId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
